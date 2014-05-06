﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

#if DEBUG
using System;
using Microsoft.AspNet.Abstractions;
using Microsoft.AspNet.Diagnostics;

namespace Microsoft.AspNet.Builder
{
    /// <summary>
    /// IBuilder extensions for the DiagnosticsPageMiddleware.
    /// </summary>
    public static class DiagnosticsPageExtensions
    {
        /// <summary>
        /// Adds the DiagnosticsPageMiddleware to the pipeline with the given options.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IBuilder UseDiagnosticsPage(this IBuilder builder, DiagnosticsPageOptions options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            return builder.Use(next => new DiagnosticsPageMiddleware(next, options).Invoke);
        }

        /// <summary>
        /// Adds the DiagnosticsPageMiddleware to the pipeline with the given path.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IBuilder UseDiagnosticsPage(this IBuilder builder, PathString path)
        {
            return UseDiagnosticsPage(builder, new DiagnosticsPageOptions { Path = path });
        }

        /// <summary>
        /// Adds the DiagnosticsPageMiddleware to the pipeline.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IBuilder UseDiagnosticsPage(this IBuilder builder)
        {
            return UseDiagnosticsPage(builder, new DiagnosticsPageOptions());
        }
    }
}
#endif