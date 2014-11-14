// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace Microsoft.AspNet.Diagnostics.Elm
{
    public class ElmLoggerProvider : ILoggerProvider
    {
        private readonly IElmStore _store;
        private readonly ElmOptions _options;

        public ElmLoggerProvider(IElmStore store, ElmOptions options)
        {
            _store = store;
            _options = options;
        }

        public ILogger Create(string name)
        {
            return new ElmLogger(name, _store, _options);
        }
    }
}
