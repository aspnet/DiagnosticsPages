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

        private LinkedList<ActivityContext> Activities { get; set; } = new LinkedList<ActivityContext>();

        public IEnumerable<LogInfo> GetLogs()
        {
            return _logs;
        }

        public IEnumerable<LogInfo> GetLogs(LogLevel minLevel)
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
        private int NumLogs()
        {
            return Activities.Sum(a => a.Size);
        }

        public IEnumerable<ActivityContext> GetActivities()
        {
            for (var context = Activities.First; context != null; context = context.Next)
            {
                if (!context.Value.IsCollapsed && CollapseActivityContext(context.Value))
                {
                    Activities.Remove(context);
                }
            }
            return Activities;
        }

        public void AddActivity(ActivityContext activity)
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

        /// <summary>
        /// Removes any nodes on the context's scope tree that doesn't have any logs
        /// This may occur as a result of the filters turned on
        /// </summary>
        /// <param name="context">The context who's node should be condensed</param>
        /// <returns>true if the node has been condensed to null, false otherwise</returns>
        private bool CollapseActivityContext(ActivityContext context)
        {
            context.Root = CollapseHelper(context.Root);
            context.IsCollapsed = true;
            return context.Root == null;
        }

        private ScopeNode CollapseHelper(ScopeNode node)
        {
            if (node == null)
            {
                return node;
            }
            for (int i = 0; i < node.Children.Count; i++)
            {
                node.Children[i] = CollapseHelper(node.Children[i]);
            }
            node.Children = node.Children.Where(c => c != null).ToList();
            if (node.Children.Count == 0 && node.Messages.Count == 0)
            {
                return null;
            }
            else
            {
                return node;
            }
        }
    }
}