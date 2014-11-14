// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Framework.Logging;

namespace Microsoft.AspNet.Diagnostics.Elm
{
    public class ElmLogger : ILogger
    {
        private readonly string _name;
        private IElmStore _store;
        private readonly ElmOptions _options;

        public ElmLogger(string name, IElmStore store, ElmOptions options)
        {
            _name = name;
            _store = store;
            _options = options;
        }

        public void Write(LogLevel logLevel, int eventId, object state, Exception exception, 
                          Func<object, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
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
            return _options.Filter(_name, logLevel);
        }

        public IDisposable BeginScope(object state)
        {
            var scope = new ElmScope(_name, state);
            scope.Context = ElmScope.Current?.Context ?? GetNewActivityContext();
            return ElmScope.Push(scope, _store);
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