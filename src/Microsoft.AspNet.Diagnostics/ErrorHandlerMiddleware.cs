// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using Microsoft.Framework.Logging;

namespace Microsoft.AspNet.Diagnostics
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ErrorHandlerOptions _options;
        private readonly ILogger _logger;
        private readonly Func<object, Task> _clearCacheHeadersDelegate;

        public ErrorHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, ErrorHandlerOptions options)
        {
            _next = next;
            _options = options;
            _logger = loggerFactory.CreateLogger<ErrorHandlerMiddleware>();
            if (_options.ErrorHandler == null)
            {
                _options.ErrorHandler = _next;
            }
            _clearCacheHeadersDelegate = ClearCacheHeaders;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unhandled exception has occurred: " + ex.Message, ex);
                // We can't do anything if the response has already started, just abort.
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the error handler will not be executed.");
                    throw;
                }

                PathString originalPath = context.Request.Path;
                if (_options.ErrorHandlingPath.HasValue)
                {
                    context.Request.Path = _options.ErrorHandlingPath;
                }
                try
                {
                    var errorHandlerFeature = new ErrorHandlerFeature()
                    {
                        Error = ex,
                    };
                    context.SetFeature<IErrorHandlerFeature>(errorHandlerFeature);
                    context.Response.StatusCode = 500;
                    context.Response.Headers.Clear();
                    context.Response.OnStarting(_clearCacheHeadersDelegate, context.Response);

                    // if buffering is enabled, then clear it as data could have been written into it.
                    if (context.Response.Body.CanSeek)
                    {
                        context.Response.Body.SetLength(0);
                    }

                    await _options.ErrorHandler(context);
                    // TODO: Optional re-throw? We'll re-throw the original exception by default if the error handler throws.
                    return;
                }
                catch (Exception ex2)
                {
                    // Suppress secondary exceptions, re-throw the original.
                    _logger.LogError("An exception was thrown attempting to execute the error handler.", ex2);
                }
                finally
                {
                    context.Request.Path = originalPath;
                }
                throw; // Re-throw the original if we couldn't handle it
            }
        }

        private Task ClearCacheHeaders(object state)
        {
            var response = (HttpResponse)state;
            response.Headers.Set("Cache-Control", "no-cache");
            response.Headers.Set("Pragma", "no-cache");
            response.Headers.Set("Expires", "-1");
            response.Headers.Remove("ETag");
            return Task.FromResult(0);
        }
    }
}