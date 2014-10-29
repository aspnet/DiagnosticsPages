// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Diagnostics.Elm.Views;
using Microsoft.Framework.Logging;
using Microsoft.Framework.OptionsModel;

namespace Microsoft.AspNet.Diagnostics.Elm
{
    /// <summary>
    /// Enables the Elm logging service
    /// </summary>
    public class ElmMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ElmOptions _options;
        private readonly ElmLoggerProvider _provider;
        private readonly IElmStore _store;
        private readonly ILogger _logger;

        public ElmMiddleware(RequestDelegate next, ILoggerFactory factory, IOptions<ElmOptions> options, IElmStore store)
        {
            _next = next;
            _options = options.Options;
            _store = store;
            _logger = factory.Create<ElmMiddleware>();
            _provider = new ElmLoggerProvider(_store);
            factory.AddProvider(_provider);
        }

        public async Task Invoke(HttpContext context)
        {
            var requestId = Guid.NewGuid();

            if (context.Request.Path != _options.Path && !context.Request.Path.StartsWithSegments(_options.Path))
            {
                using (_logger.BeginScope(string.Format("request {0}", requestId)))
                {
                    try
                    {
                        ElmScope.Current.Context.HttpInfo = GetHttpInfo(context, requestId);
                        await _next(context);
                        return;
                    }
                    catch (Exception ex)
                    {
                        ElmScope.Current.Context.HttpInfo = GetHttpInfo(context, requestId);
                        _logger.WriteError("An unhandled exception has occurred: " + ex.Message, ex);
                        throw;
                    }
                }
            }

            // parse params
            var logs = (IEnumerable<LogInfo>)null;
            if (context.Request.Query.ContainsKey("level"))
            {
                var minLevel = (TraceType)int.Parse(context.Request.Query.GetValues("level")[0]);
                logs = _store.GetLogs(minLevel);
                _options.MinLevel = minLevel;
            }
            else
            {
                logs = _store.GetLogs();
                _options.MinLevel = TraceType.Verbose;
            }
            if (context.Request.Query.ContainsKey("name"))
            {
                var namePrefix = context.Request.Query.GetValues("name")[0];
                logs = logs.Where(l => l.Name.StartsWith(namePrefix));
                _options.NamePrefix = namePrefix;
            }

            // main log page
            if (context.Request.Path == _options.Path)
            {

                var model = new LogPageModel()
                {
                    // sort so most recent logs are first
                    Logs = logs.OrderBy(l => l.Time).Reverse(),
                    LogTree = ElmStore.Activities,
                    Options = _options
                };
                var logPage = new LogPage(model);



                await logPage.ExecuteAsync(context);
            }
            // request details page
            else
            {
                try
                {
                    var parts = context.Request.Path.Value.Split('/');
                    var id = Guid.Parse(parts[parts.Length - 1]);
                    var requestLogs = logs.Where(l => l.ActivityContext.HttpInfo != null ? l.ActivityContext.HttpInfo.RequestID == id : false);
                    var model = new RequestPageModel()
                    {
                        RequestID = id,
                        Logs = requestLogs,
                        Options = _options
                    };
                    var requestPage = new RequestPage(model);
                    await requestPage.ExecuteAsync(context);
                }
                catch (Exception)
                {
                    // TODO: bad url
                }
            }
        }

        /// <summary>
        /// Takes the info from the given HttpContext and copies it to an HttpInfo object
        /// </summary>
        /// <returns>The HttpInfo for the current elm context</returns>
        private HttpInfo GetHttpInfo(HttpContext context, Guid requestId)
        {
            return new HttpInfo()
            {
                RequestID = requestId,
                Host = context.Request.Host,
                ContentType = context.Request.ContentType,
                Path = context.Request.Path,
                Scheme = context.Request.Scheme,
                StatusCode = context.Response.StatusCode,
                User = context.User,
                Method = context.Request.Method,
                Protocol = context.Request.Protocol,
                Headers = context.Request.Headers,
                Query = context.Request.QueryString,
                Cookies = context.Request.Cookies
            };
        }
    }
}