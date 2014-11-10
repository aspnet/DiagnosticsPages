﻿using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.Diagnostics.Elm
{
    public class ScopeNode
    {
        public ScopeNode Parent { get; set; }
        public List<ScopeNode> Children { get; set; } = new List<ScopeNode>();
        public List<LogInfo> Messages { get; set; } = new List<LogInfo>();
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public object State { get; set; }
        public string Name { get; set; }
    }
}