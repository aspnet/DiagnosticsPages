// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Diagnostics.Elm;
using Microsoft.Framework.DependencyInjection;

namespace Microsoft.AspNet.Builder
{
    public static class ElmExtensions
    {
        public static IApplicationBuilder UseElmCapture(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ElmCaptureMiddleware>();
        }

        public static IApplicationBuilder UseElmPage(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ElmPageMiddleware>();
        }

        public static IServiceCollection AddElm(this IServiceCollection services, Action<ElmOptions> configureOptions = null)
        {
            services.AddSingleton<IElmStore, ElmStore>(); // registering the service so it can be injected into constructors
            return services.Configure(configureOptions);
        }
    }
}