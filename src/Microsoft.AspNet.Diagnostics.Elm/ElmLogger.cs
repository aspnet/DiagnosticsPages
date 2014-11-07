// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
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

        public void Write(LogLevel logLevel, int eventId, object state, Exception exception, 
                          Func<object, Exception, string> formatter)
        {
            LogInfo info = new LogInfo()
            {
                ActivityContext = GetCurrentActivityContext(),
                Name = _name,
                EventID = eventId,
                Severity = logLevel,
                Exception = exception,
                State = state,
                Time = DateTimeOffset.UtcNow
            };
            if (ElmScope.Current != null)
            {
                GetCurrentActivityContext().Size++;
                ElmScope.Current.Node.Messages.Add(info);
            }
            _store.Add(info);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope(object state)
        {
            var scope = new ElmScope(_name, state);
            scope.Context = ElmScope.Current?.Context ?? GetNewActivityContext();
            return ElmScope.Push(scope);
        }

        private ActivityContext GetNewActivityContext()
        {
            return new ActivityContext()
            {
                Id = Guid.NewGuid(),
                Time = DateTimeOffset.UtcNow
            };
        }

        private ActivityContext GetCurrentActivityContext()
        {
            return ElmScope.Current?.Context ?? GetNewActivityContext();
        }
    }
}