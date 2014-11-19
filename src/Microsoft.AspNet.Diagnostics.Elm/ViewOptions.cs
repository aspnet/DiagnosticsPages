﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Framework.Logging;

namespace Microsoft.AspNet.Diagnostics.Elm
{
    /// <summary>
    /// Options for viewing elm logs.
    /// </summary>
    public class ViewOptions
    {
        /// <summary>
        /// The minimum <see cref="LogLevel"/> of logs shown on the elm page.
        /// </summary>
        public LogLevel MinLevel { get; set; }

        /// <summary>
        /// The prefix for the logger names of logs shown on the elm page.
        /// </summary>
        public string NamePrefix { get; set; }
    }
}