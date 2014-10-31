using System.Collections.Generic;
using Microsoft.AspNet.Http;

namespace Microsoft.AspNet.Diagnostics.Elm.Views
{
    public class LogPageModel
    {
        public IEnumerable<LogInfo> Logs { get; set; }

        public IDictionary<ActivityContext, ScopeNode> LogTree { get; set; }

        public ViewOptions Options { get; set; }

        public PathString Path { get; set; }
    }
}