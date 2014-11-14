﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Http;
using Microsoft.Framework.Logging;

namespace Microsoft.AspNet.Diagnostics.Elm
{
    /// <summary>
    /// Options for ElmMiddleware
    /// </summary>
    public class ElmOptions
    {
        /// <summary>
        /// Specifies the path to view the logs
        /// </summary>
        public PathString Path { get; set; } = new PathString("/Elm");

        public Func<string, LogLevel, bool> Filter { get; set; } = (name, level) => true;
    }
}