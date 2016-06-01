// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.Views;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Diagnostics
{
    /// <summary>
    /// Captures synchronous and asynchronous exceptions from the pipeline and generates HTML error responses.
    /// </summary>
    public class DeveloperExceptionPageMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly DeveloperExceptionPageOptions _options;
        private static readonly bool IsMono = Type.GetType("Mono.Runtime") != null;
        private readonly ILogger _logger;
        private readonly IFileProvider _fileProvider;
        private readonly DiagnosticSource _diagnosticSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeveloperExceptionPageMiddleware"/> class
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="hostingEnvironment"></param>
        /// <param name="diagnosticSource"></param>
        public DeveloperExceptionPageMiddleware(
            RequestDelegate next,
            IOptions<DeveloperExceptionPageOptions> options,
            ILoggerFactory loggerFactory,
            IHostingEnvironment hostingEnvironment,
            DiagnosticSource diagnosticSource)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _next = next;
            _options = options.Value;
            _logger = loggerFactory.CreateLogger<DeveloperExceptionPageMiddleware>();
            _fileProvider = _options.FileProvider ?? hostingEnvironment.ContentRootFileProvider;
            _diagnosticSource = diagnosticSource;
        }

        /// <summary>
        /// Process an individual request.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex, "An unhandled exception has occurred while executing the request");

                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the error page middleware will not be executed.");
                    throw;
                }

                try
                {
                    context.Response.Clear();
                    context.Response.StatusCode = 500;

                    await DisplayException(context, ex);

                    if (_diagnosticSource.IsEnabled("Microsoft.AspNetCore.Diagnostics.UnhandledException"))
                    {
                        _diagnosticSource.Write("Microsoft.AspNetCore.Diagnostics.UnhandledException", new { httpContext = context, exception = ex });
                    }

                    return;
                }
                catch (Exception ex2)
                {
                    // If there's a Exception while generating the error page, re-throw the original exception.
                    _logger.LogError(0, ex2, "An exception was thrown attempting to display the error page.");
                }
                throw;
            }
        }

        // Assumes the response headers have not been sent.  If they have, still attempt to write to the body.
        private Task DisplayException(HttpContext context, Exception ex)
        {
            var compilationException = ex as ICompilationException;
            if (compilationException != null)
            {
                return DisplayCompilationException(context, compilationException);
            }

            return DisplayRuntimeException(context, ex);
        }

        private Task DisplayCompilationException(
            HttpContext context,
            ICompilationException compilationException)
        {
            var model = new CompilationErrorPageModel
            {
                Options = _options,
            };

            foreach (var compilationFailure in compilationException.CompilationFailures)
            {
                var stackFrames = new List<StackFrameModel>();
                var errorDetails = new ErrorDetails
                {
                    StackFrames = stackFrames
                };
                var fileContent = compilationFailure
                    .SourceFileContent
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                foreach (var item in compilationFailure.Messages)
                {
                    var frame = new StackFrameModel
                    {
                        File = compilationFailure.SourceFilePath,
                        Line = item.StartLine,
                        Function = string.Empty
                    };

                    ReadFrameContent(frame, fileContent, item.StartLine, item.EndLine);
                    frame.ErrorDetails = item.Message;

                    stackFrames.Add(frame);
                }

                model.ErrorDetails.Add(errorDetails);
            }

            var errorPage = new CompilationErrorPage
            {
                Model = model
            };

            return errorPage.ExecuteAsync(context);
        }

        private Task DisplayRuntimeException(HttpContext context, Exception ex)
        {
            var request = context.Request;

            var details = GetErrorDetails(ex);
            details.Reverse();

            var model = new ErrorPageModel
            {
                Options = _options,
                ErrorDetails = details,
                Query = request.Query,
                Cookies = request.Cookies,
                Headers = request.Headers
            };

            var errorPage = new ErrorPage(model);
            return errorPage.ExecuteAsync(context);
        }

        private List<ErrorDetails> GetErrorDetails(Exception ex)
        {
            var details = new List<ErrorDetails>();
            var pdbReader = new PortablePdbReader();

            for (var scan = ex; scan != null; scan = scan.InnerException)
            {
                details.Add(new ErrorDetails
                {
                    Error = scan,
                    StackFrames = StackFrames(scan, pdbReader)
                });
            }

            return details;
        }

        private List<StackFrameModel> StackFrames(Exception ex, PortablePdbReader pdbReader)
        {
            var frames = new List<StackFrameModel>();
            var stackTrace = new StackTrace(ex, true);

            foreach (var frame in stackTrace.GetFrames())
            {
                var model = GetStackFrameModel(frame, pdbReader);

                frames.Add(GetStackFrame(model));
            }

            return frames;
        }

        internal StackFrameModel GetStackFrame(string function, string fileName, int lineNumber)
        {
            return GetStackFrame(new StackFrameModel { Function = function, File = fileName, Line = lineNumber });
        }

        // make it internal to enable unit testing
        private StackFrameModel GetStackFrame(StackFrameModel frame)
        {
            var file = frame.File;
            var lineNumber = frame.Line;

            if (string.IsNullOrEmpty(file))
            {
                return frame;
            }

            IEnumerable<string> lines = null;
            if (File.Exists(file))
            {
                lines = File.ReadLines(file);
            }
            else
            {
                // Handle relative paths and embedded files
                var fileInfo = _fileProvider.GetFileInfo(file);
                if (fileInfo.Exists)
                {
                    // ReadLines doesn't accept a stream. Use ReadLines as its more efficient
                    // relative to reading lines via stream reader
                    if (!string.IsNullOrEmpty(fileInfo.PhysicalPath))
                    {
                        lines = File.ReadLines(fileInfo.PhysicalPath);
                    }
                    else
                    {
                        lines = ReadLines(fileInfo);
                    }
                }
            }

            if (lines != null)
            {
                ReadFrameContent(frame, lines, lineNumber, lineNumber);
            }

            return frame;
        }

        // make it internal to enable unit testing
        internal void ReadFrameContent(
            StackFrameModel frame,
            IEnumerable<string> allLines,
            int errorStartLineNumberInFile,
            int errorEndLineNumberInFile)
        {
            // Get the line boundaries in the file to be read and read all these lines at once into an array.
            var preErrorLineNumberInFile = Math.Max(errorStartLineNumberInFile - _options.SourceCodeLineCount, 1);
            var postErrorLineNumberInFile = errorEndLineNumberInFile + _options.SourceCodeLineCount;
            var codeBlock = allLines
                .Skip(preErrorLineNumberInFile - 1)
                .Take(postErrorLineNumberInFile - preErrorLineNumberInFile + 1)
                .ToArray();

            var numOfErrorLines = (errorEndLineNumberInFile - errorStartLineNumberInFile) + 1;
            var errorStartLineNumberInArray = errorStartLineNumberInFile - preErrorLineNumberInFile;

            frame.PreContextLine = preErrorLineNumberInFile;
            frame.PreContextCode = codeBlock.Take(errorStartLineNumberInArray).ToArray();
            frame.ContextCode = codeBlock
                .Skip(errorStartLineNumberInArray)
                .Take(numOfErrorLines)
                .ToArray();
            frame.PostContextCode = codeBlock
                .Skip(errorStartLineNumberInArray + numOfErrorLines)
                .ToArray();
        }

        private static IEnumerable<string> ReadLines(IFileInfo fileInfo)
        {
            using (var reader = new StreamReader(fileInfo.CreateReadStream()))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        private StackFrameModel GetStackFrameModel(StackFrame frame, PortablePdbReader pdbReader)
        {
            var lineNumber = frame.GetFileLineNumber();
            var fileName = frame.GetFileName();
            var method = frame.GetMethod();

            var model = new StackFrameModel
            {
                Function = method.Name,
                Line = lineNumber,
                File = fileName
            };

            if (string.IsNullOrEmpty(model.File))
            {
#if NET451
                // .NET Framework and older versions of mono don't support portable PDBs
                // so we read it manually to get file name and line information
                pdbReader.PopulateStackFrame(model, method, frame.GetILOffset());
#endif
            }

            return model;
        }

        private class PortablePdbReader
        {
#if NET451
            public void PopulateStackFrame(StackFrameModel model, MethodBase method, int IlOffset)
            {
                var pdbPath = GetPdbPath(method.Module.Assembly.Location);

                if (string.IsNullOrEmpty(pdbPath))
                {
                    return;
                }

                // REVIEW: Performance? Should we just cache this information?
                using (var pdbStream = File.OpenRead(pdbPath))
                {
                    using (var metadataReaderProvider = MetadataReaderProvider.FromPortablePdbStream(pdbStream))
                    {
                        var metadataReader = metadataReaderProvider.GetMetadataReader();
                        var methodToken = MetadataTokens.Handle(method.MetadataToken);

                        Debug.Assert(methodToken.Kind == HandleKind.MethodDefinition);

                        var handle = ((MethodDefinitionHandle)methodToken).ToDebugInformationHandle();

                        if (!handle.IsNil)
                        {
                            var methodDebugInfo = metadataReader.GetMethodDebugInformation(handle);
                            var sequencePoints = methodDebugInfo.GetSequencePoints();
                            SequencePoint? bestPointSoFar = null;

                            foreach (var point in sequencePoints)
                            {
                                if (point.Offset > IlOffset)
                                {
                                    break;
                                }

                                if (point.StartLine != SequencePoint.HiddenLine)
                                {
                                    bestPointSoFar = point;
                                }
                            }

                            if (bestPointSoFar.HasValue)
                            {
                                model.Line = bestPointSoFar.Value.StartLine;
                                model.File = metadataReader.GetString(metadataReader.GetDocument(bestPointSoFar.Value.Document).Name);
                            }
                        }
                    }
                }
            }

            private static string GetPdbPath(string assemblyPath)
            {
                if (string.IsNullOrEmpty(assemblyPath))
                {
                    return null;
                }

                if (File.Exists(assemblyPath))
                {
                    var peStream = File.OpenRead(assemblyPath);

                    using (var peReader = new PEReader(peStream))
                    {
                        foreach (var entry in peReader.ReadDebugDirectory())
                        {
                            if (entry.Type == DebugDirectoryEntryType.CodeView)
                            {
                                var codeViewData = peReader.ReadCodeViewDebugDirectoryData(entry);

                                string peDirectory = Path.GetDirectoryName(assemblyPath);
                                return Path.Combine(peDirectory, Path.GetFileName(codeViewData.Path));
                            }
                        }
                    }
                }

                return null;
            }
#endif
        }
    }
}
