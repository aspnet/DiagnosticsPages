// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.Framework.Logging;

namespace Microsoft.AspNet.Diagnostics.Elm
{
    public class ElmStore : IElmStore
    {
        private const int Capacity = 200;

        private readonly Queue<LogInfo> _logs = new Queue<LogInfo>();

        private static LinkedList<ActivityContext> Activities { get; set; } = new LinkedList<ActivityContext>();

        public IEnumerable<LogInfo> GetLogs()
        {
            return _logs;
        }

        public IEnumerable<LogInfo> GetLogs(TraceType minLevel)
        {
            return _logs.Where(l => l.Severity >= minLevel);
        }

        public void Add(LogInfo info)
        {
            lock (_logs)
            {
                _logs.Enqueue(info);
                while (_logs.Count > Capacity)
                {
                    _logs.Dequeue();
                }
            }
        }

        // returns the number of log messages stored in Activities
        private static int NumLogs()
        {
            return Activities.Sum(a => a.Size);
        }

        public static IEnumerable<ActivityContext> GetActivities()
        {
            return Activities;
        }

        public static void AddActivity(ActivityContext activity)
        {
            lock (Activities)
            {
                Activities.AddLast(activity);
                while (NumLogs() > Capacity)
                {
                    Activities.RemoveFirst();
                }
            }
        }
    }
}