// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.


namespace Microsoft.AspNet.Diagnostics
{
    /// <summary>
    /// Options for the ErrorPageMiddleware.
    /// </summary>
    public class ErrorPageOptions
    {
        /// <summary>
        /// Create an instance with the default options settings.
        /// </summary>
        public ErrorPageOptions()
        {
            SourceCodeLineCount = 6;
        }

        /// <summary>
        /// Determines how many lines of code to include before and after the line of code
        /// present in an exception's stack frame. Only applies when symbols are available and
        /// source code referenced by the exception stack trace is present on the server.
        /// </summary>
        public int SourceCodeLineCount { get; set; }
    }
}
