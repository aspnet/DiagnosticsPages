using System.Collections.Generic;

namespace Microsoft.AspNet.Diagnostics.Elm.Views
{
    public class LogPageModel
    {
        public IEnumerable<LogInfo> Logs { get; set; }

        public IDictionary<ActivityContext, ScopeNode> LogTree { get; set; }

        public ElmOptions Options { get; set; }
    }
}