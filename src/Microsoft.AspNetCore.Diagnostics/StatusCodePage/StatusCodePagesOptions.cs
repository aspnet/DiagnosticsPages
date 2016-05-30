// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Options for StatusCodePagesMiddleware.
    /// </summary>
    public class StatusCodePagesOptions
    {
        public StatusCodePagesOptions()
        {
            HandleAsync = context =>
            {
                // TODO: Render with a pre-compiled html razor view.
                // Note the 500 spaces are to work around an IE 'feature'
                var statusCode = context.HttpContext.Response.StatusCode;

                var body = BuildResponseBody(statusCode);

                context.HttpContext.Response.ContentType = "text/plain";
                return context.HttpContext.Response.WriteAsync(body);
            };
        }

        private string BuildResponseBody(int httpStatusCode)
        {
            string internetExplorerWorkaround = new string(' ', 500);
            var reasonPhrase = ReasonPhrases.GetReasonPhrase(httpStatusCode);

            if (string.IsNullOrWhiteSpace(reasonPhrase))
            {
                return string.Format(CultureInfo.InvariantCulture, "Status Code: {0} {1}", httpStatusCode, internetExplorerWorkaround);
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture, "Status Code: {0}; {1}{2}", httpStatusCode, reasonPhrase, internetExplorerWorkaround);
            }
        }

        public Func<StatusCodeContext, Task> HandleAsync { get; set; }
    }
}