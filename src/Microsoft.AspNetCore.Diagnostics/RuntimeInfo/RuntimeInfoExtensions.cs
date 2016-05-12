// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Builder
{
    // Temporarily disabling these extensions https://github.com/aspnet/Diagnostics/issues/279
    static class RuntimeInfoExtensions
    {
        public static IApplicationBuilder UseRuntimeInfoPage(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<RuntimeInfoMiddleware>();
        }

        public static IApplicationBuilder UseRuntimeInfoPage(this IApplicationBuilder app, string path)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseRuntimeInfoPage(new RuntimeInfoPageOptions
            {
                Path = new PathString(path)
            });
        }

        public static IApplicationBuilder UseRuntimeInfoPage(
            this IApplicationBuilder app,
            RuntimeInfoPageOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return app.UseMiddleware<RuntimeInfoMiddleware>(Options.Create(options));
        }
    }
}
