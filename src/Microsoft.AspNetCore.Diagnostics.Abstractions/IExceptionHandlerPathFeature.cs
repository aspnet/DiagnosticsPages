// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNetCore.Diagnostics
{
    /// <summary>
    /// Represents an exception handler with the original path of the request.
    /// </summary>
    public interface IExceptionHandlerPathFeature : IExceptionHandlerFeature
    {
        /// <summary>
        /// The path of request that caused the exception. The value is un-escaped.
        /// </summary>
        string Path { get; }
    }
}
