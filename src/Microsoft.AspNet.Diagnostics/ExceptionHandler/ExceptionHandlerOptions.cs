// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace Microsoft.AspNet.Diagnostics
{
    public class ExceptionHandlerOptions
    {
        public PathString ExceptionHandlingPath { get; set; }

        public RequestDelegate ExceptionHandler { get; set; }
    }
}