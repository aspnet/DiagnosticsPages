// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Framework.Logging;

namespace Microsoft.AspNet.Diagnostics.Elm
{
    /// <summary>
    /// Options for ElmMiddleware
    /// </summary>
    public class ViewOptions
    {
        /// <summary>
        /// The minimum severity level shown
        /// </summary>
        public LogLevel MinLevel { get; set; }

        /// <summary>
        /// prefix filter for the loggers shown
        /// </summary>
        public string NamePrefix { get; set; }
    }
}