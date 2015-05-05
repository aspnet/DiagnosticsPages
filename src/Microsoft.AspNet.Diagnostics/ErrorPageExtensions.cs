// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Diagnostics;

namespace Microsoft.AspNet.Builder
{
    /// <summary>
    /// IApplicationBuilder extension methods for the ErrorPageMiddleware.
    /// </summary>
    public static class ErrorPageExtensions
    {
        /// <summary>
        /// Captures synchronous and asynchronous exceptions from the pipeline and generates HTML error responses.
        /// Full error details are only displayed by default if 'host.AppMode' is set to 'development' in the IApplicationBuilder.Properties.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseErrorPage(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseErrorPage(new ErrorPageOptions());
        }

        /// <summary>
        /// Captures synchronous and asynchronous exceptions from the pipeline and generates HTML error responses.
        /// Full error details are only displayed by default if 'host.AppMode' is set to 'development' in the IApplicationBuilder.Properties.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseErrorPage(this IApplicationBuilder builder, ErrorPageOptions options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            /* TODO: Development, Staging, or Production
            string appMode = new AppProperties(builder.Properties).Get<string>(Constants.HostAppMode);
            bool isDevMode = string.Equals(Constants.DevMode, appMode, StringComparison.Ordinal);*/
            bool isDevMode = true;
            return builder.Use(next => new ErrorPageMiddleware(next, options, isDevMode).Invoke);
        }
    }
}