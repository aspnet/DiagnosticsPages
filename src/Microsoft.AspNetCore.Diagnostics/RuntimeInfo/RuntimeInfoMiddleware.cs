// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Diagnostics
{
    /// <summary>
    /// Displays information about the packages used by the application at runtime
    /// </summary>
    public class RuntimeInfoMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RuntimeInfoPageOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeInfoMiddleware"/> class
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        public RuntimeInfoMiddleware(
            RequestDelegate next,
            IOptions<RuntimeInfoPageOptions> options)
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
        }

        /// <summary>
        /// Process an individual request.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {
            var request = context.Request;
            if (!_options.Path.HasValue || _options.Path == request.Path)
            {
                var model = CreateRuntimeInfoModel();
                var runtimeInfoPage = new RuntimeInfoPage(model);
                return runtimeInfoPage.ExecuteAsync(context);
            }

            return _next(context);
        }

        internal RuntimeInfoPageModel CreateRuntimeInfoModel()
        {
            var model = new RuntimeInfoPageModel();
            model.Version = typeof(object).GetTypeInfo().Assembly.GetName().Version.ToString();
            model.OperatingSystem = RuntimeInformation.OSDescription;
#if NET451
            model.RuntimeType = "CLR";
#else
            model.RuntimeType = "CoreCLR";
#endif
            model.RuntimeArchitecture = RuntimeInformation.ProcessArchitecture.ToString();
            return model;
        }
    }
}