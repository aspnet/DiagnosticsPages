using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.Diagnostics.Elm.Views
{
    public class RequestPageModel
    {
        public Guid RequestID { get; set; }
        
        public IEnumerable<LogInfo> Logs { get; set; }

        public ViewOptions Options { get; set; }
    }
}