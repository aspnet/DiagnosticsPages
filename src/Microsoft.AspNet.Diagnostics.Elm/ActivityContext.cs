using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.Diagnostics.Elm
{
    public class ActivityContext
    {
        public Guid Id { get; set; }

        public HttpInfo HttpInfo { get; set; }

        public ScopeNode Root { get; set; }

        public DateTimeOffset Time { get; set; }

        public List<LogInfo> AllMessages { get; private set; } = new List<LogInfo>();

        public bool IsCollapsed { get; set; }
    }
}