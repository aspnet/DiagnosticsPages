// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Diagnostics.Elm.Views;
using Microsoft.Framework.Logging;
using Microsoft.Framework.OptionsModel;

namespace Microsoft.AspNet.Diagnostics.Elm
{
    /// <summary>
    /// Enables viewing logs captured by the <see cref="ElmCaptureMiddleware"/>.
    /// </summary>
    public class ElmPageMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ElmOptions _options;
        private readonly ElmStore _store;

        public ElmPageMiddleware(RequestDelegate next, IOptions<ElmOptions> options, ElmStore store)
        {
            _next = next;
            _options = options.Options;
            _store = store;
        }

        public async Task Invoke(HttpContext context)
        {

            if (context.Request.Path != _options.Path && !context.Request.Path.StartsWithSegments(_options.Path))
            {
                await _next(context);
                return;
            }

            // parse params
            var options = new ViewOptions()
            {
                MinLevel = LogLevel.Verbose,
                NamePrefix = ""
            };
            if (context.Request.Query.ContainsKey("level"))
            {
                var minLevel = options.MinLevel;
                if (Enum.TryParse<LogLevel>(context.Request.Query.GetValues("level")[0], out minLevel))
                {
                    options.MinLevel = minLevel;
                }
            }
            if (context.Request.Query.ContainsKey("name"))
            {
                var namePrefix = context.Request.Query.GetValues("name")[0];
                options.NamePrefix = namePrefix;
            }

            // main log page
            if (context.Request.Path == _options.Path)
            {
                var model = new LogPageModel()
                {
                    // sort so most recent logs are first
                    Activities = _store.GetActivities(),
                    Options = options,
                    Path = _options.Path
                };
                var logPage = new LogPage(model);

                await logPage.ExecuteAsync(context);
            }
            // request details page
            else
            {
                var parts = context.Request.Path.Value.Split('/');
                var id = Guid.Empty;
                if (!Guid.TryParse(parts[parts.Length - 1], out id))
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid Request Id");
                    return;
                }
                var requestLogs = _store.GetActivityLogs(id, options.MinLevel);
                var model = new RequestPageModel()
                {
                    RequestID = id,
                    Logs = requestLogs,
                    Options = options
                };
                var requestPage = new RequestPage(model);
                await requestPage.ExecuteAsync(context);
            }
        }
    }
}