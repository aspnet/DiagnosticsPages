// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.Framework.Logging;

namespace Microsoft.AspNet.Diagnostics.Elm
{
    public class ElmLogger : ILogger
    {
        private readonly string _name;
        private readonly ElmLoggerProvider _provider;
        private IElmStore _store;

        public ElmLogger(string name, ElmLoggerProvider provider, IElmStore store)
        {
            _name = name;
            _provider = provider;
            _store = store;
        }

        public void Write(TraceType traceType, int eventId, object state, Exception exception, 
                          Func<object, Exception, string> formatter)
        {
            LogInfo info = new LogInfo()
            {
                ActivityContext = ElmScope.Current == null ? GetNewActivityContext() : ElmScope.Current.Context,
                Name = _name,
                EventID = eventId,
                Severity = traceType,
                Exception = exception,
                State = state,
                Time = DateTimeOffset.UtcNow
            };
            ElmScope.Current?.Node?.Messages?.Add(info);
            _store.Add(info);
        }

        public bool IsEnabled(TraceType traceType)
        {
            return true;
        }

        public IDisposable BeginScope(object state)
        {
            var scope = new ElmScope(_name, state);
            if (ElmScope.Current != null)
            {
                scope.Context = ElmScope.Current.Context;
            }
            else
            {
                scope.Context = GetNewActivityContext();
            }
            return ElmScope.Push(scope);
        }

        private ActivityContext GetNewActivityContext()
        {
            return new ActivityContext()
            {
                Id = Guid.NewGuid()
            };
        }
    }
}