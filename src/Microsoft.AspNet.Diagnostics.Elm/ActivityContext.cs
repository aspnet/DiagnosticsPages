using System;

namespace Microsoft.AspNet.Diagnostics.Elm
{
    public class ActivityContext
    {
        public Guid Id { get; set; }
        public HttpInfo HttpInfo { get; set; }
    }
}