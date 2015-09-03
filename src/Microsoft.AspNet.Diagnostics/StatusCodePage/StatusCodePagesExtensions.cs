// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using Microsoft.Framework.Internal;

namespace Microsoft.AspNet.Builder
{
    public static class StatusCodePagesExtensions
    {
        /// <summary>
        /// Adds a StatusCodePages middleware with the given options that checks for responses with status codes 
        /// between 400 and 599 that do not have a body.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseStatusCodePages([NotNull]this IApplicationBuilder app, StatusCodePagesOptions options)
        {
            return app.UseMiddleware<StatusCodePagesMiddleware>(options);
        }

        /// <summary>
        /// Adds a StatusCodePages middleware with a default response handler that checks for responses with status codes 
        /// between 400 and 599 that do not have a body.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseStatusCodePages(this IApplicationBuilder app)
        {
            return UseStatusCodePages(app, new StatusCodePagesOptions());
        }

        /// <summary>
        /// Adds a StatusCodePages middleware with the specified handler that checks for responses with status codes 
        /// between 400 and 599 that do not have a body.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseStatusCodePages(this IApplicationBuilder app, Func<StatusCodeContext, Task> handler)
        {
            return UseStatusCodePages(app, new StatusCodePagesOptions() { HandleAsync = handler });
        }

        /// <summary>
        /// Adds a StatusCodePages middleware with the specified response body to send. This may include a '{0}' placeholder for the status code.
        /// The middleware checks for responses with status codes between 400 and 599 that do not have a body.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="contentType"></param>
        /// <param name="bodyFormat"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseStatusCodePages(this IApplicationBuilder app, string contentType, string bodyFormat)
        {
            return UseStatusCodePages(app, context =>
            {
                var body = string.Format(CultureInfo.InvariantCulture, bodyFormat, context.HttpContext.Response.StatusCode);
                context.HttpContext.Response.ContentType = contentType;
                return context.HttpContext.Response.WriteAsync(body);
            });
        }

        /// <summary>
        /// Adds a StatusCodePages middleware to the pipeine. Specifies that responses should be handled by redirecting 
        /// with the given location URL template. This may include a '{0}' placeholder for the status code. URLs starting 
        /// with '~' will have PathBase prepended, where any other URL will be used as is.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="locationFormat"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseStatusCodePagesWithRedirects(this IApplicationBuilder app, string locationFormat)
        {
            if (locationFormat.StartsWith("~"))
            {
                locationFormat = locationFormat.Substring(1);
                return UseStatusCodePages(app, context =>
                {
                    var location = string.Format(CultureInfo.InvariantCulture, locationFormat, context.HttpContext.Response.StatusCode);
                    context.HttpContext.Response.Redirect(context.HttpContext.Request.PathBase + location);
                    return Task.FromResult(0);
                });
            }
            else
            {
                return UseStatusCodePages(app, context =>
                {
                    var location = string.Format(CultureInfo.InvariantCulture, locationFormat, context.HttpContext.Response.StatusCode);
                    context.HttpContext.Response.Redirect(location);
                    return Task.FromResult(0);
                });
            }
        }

        /// <summary>
        /// Adds a StatusCodePages middleware to the pipeline with the specified alternate middleware pipeline to execute 
        /// to generate the response body.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseStatusCodePages(this IApplicationBuilder app, Action<IApplicationBuilder> configuration)
        {
            var builder = app.New();
            configuration(builder);
            var tangent = builder.Build();
            return UseStatusCodePages(app, context => tangent(context.HttpContext));
        }

        /// <summary>
        /// Adds a StatusCodePages middleware to the pipeline. Specifies that the response body should be generated by 
        /// re-executing the request pipeline using an alternate path. This path may contain a '{0}' placeholder of the status code.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="pathFormat"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseStatusCodePagesWithReExecute(this IApplicationBuilder app, string pathFormat)
        {
            return UseStatusCodePages(app, async context =>
            {
                var newPath = new PathString(string.Format(CultureInfo.InvariantCulture, pathFormat, context.HttpContext.Response.StatusCode));

                var originalPath = context.HttpContext.Request.Path;
                // Store the original paths so the app can check it.
                context.HttpContext.Features.Set<IStatusCodeReExecuteFeature>(new StatusCodeReExecuteFeature()
                {
                    OriginalPathBase = context.HttpContext.Request.PathBase.Value,
                    OriginalPath = originalPath.Value,
                });

                context.HttpContext.Request.Path = newPath;
                try
                {
                    await context.Next(context.HttpContext);
                }
                finally
                {
                    context.HttpContext.Request.Path = originalPath;
                    context.HttpContext.Features.Set<IStatusCodeReExecuteFeature>(null);
                }
            });
        }
    }
}