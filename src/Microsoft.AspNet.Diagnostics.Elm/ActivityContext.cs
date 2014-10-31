using System;

namespace Microsoft.AspNet.Diagnostics.Elm
{
    public class ActivityContext
    {
        public Guid Id { get; set; }
        public HttpInfo HttpInfo { get; set; }
        public ScopeNode Root { get; set; }
        public DateTimeOffset Time { get; set; }
        public int Size { get; set; }
    }
}