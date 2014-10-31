namespace Microsoft.AspNet.Diagnostics.Elm.Views
{
#line 1 "LogPage.cshtml"
using System

#line default
#line hidden
    ;
#line 2 "LogPage.cshtml"
using System.Globalization

#line default
#line hidden
    ;
#line 3 "LogPage.cshtml"
using System.Linq

#line default
#line hidden
    ;
#line 4 "LogPage.cshtml"
using Microsoft.AspNet.Diagnostics.Elm.Views

#line default
#line hidden
    ;
#line 5 "LogPage.cshtml"
using Microsoft.Framework.Logging

#line default
#line hidden
    ;
#line 6 "LogPage.cshtml"
using Microsoft.AspNet.Diagnostics.Elm

#line default
#line hidden
    ;
#line 7 "LogPage.cshtml"
using Microsoft.AspNet.Diagnostics.Views

#line default
#line hidden
    ;
    using System.Threading.Tasks;

    public class LogPage : Microsoft.AspNet.Diagnostics.Views.BaseView
    {
public  HelperResult 
#line 20 "LogPage.cshtml"
LogRow(LogInfo log, int level) {

#line default
#line hidden
        return new HelperResult((__razor_helper_writer) => {
#line 20 "LogPage.cshtml"
                                        
    if (log.Severity >= Model.Options.MinLevel && 
        (string.IsNullOrEmpty(Model.Options.NamePrefix) || log.Name.StartsWith(Model.Options.NamePrefix)))
    {

#line default
#line hidden

            WriteLiteralTo(__razor_helper_writer, "        <tr class=\"logRow\">\r\n            <td>");
#line 25 "LogPage.cshtml"
WriteTo(__razor_helper_writer, string.Format("{0:MM/dd/yy}", log.Time));

#line default
#line hidden
            WriteLiteralTo(__razor_helper_writer, "</td>\r\n            <td>");
#line 26 "LogPage.cshtml"
WriteTo(__razor_helper_writer, string.Format("{0:H:mm:ss}", log.Time));

#line default
#line hidden
            WriteLiteralTo(__razor_helper_writer, "</td>\r\n            <td");
            WriteAttributeTo(__razor_helper_writer, "title", Tuple.Create(" title=\"", 810), Tuple.Create("\"", 827), 
            Tuple.Create(Tuple.Create("", 818), Tuple.Create<System.Object, System.Int32>(log.Name, 818), false));
            WriteLiteralTo(__razor_helper_writer, ">");
#line 27 "LogPage.cshtml"
    WriteTo(__razor_helper_writer, log.Name);

#line default
#line hidden
            WriteLiteralTo(__razor_helper_writer, "</td>\r\n            <td");
            WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 860), Tuple.Create("\"", 881), 
            Tuple.Create(Tuple.Create("", 868), Tuple.Create<System.Object, System.Int32>(log.Severity, 868), false));
            WriteLiteralTo(__razor_helper_writer, ">");
#line 28 "LogPage.cshtml"
        WriteTo(__razor_helper_writer, log.Severity);

#line default
#line hidden
            WriteLiteralTo(__razor_helper_writer, "</td>\r\n            <td");
            WriteAttributeTo(__razor_helper_writer, "title", Tuple.Create(" title=\"", 918), Tuple.Create("\"", 936), 
            Tuple.Create(Tuple.Create("", 926), Tuple.Create<System.Object, System.Int32>(log.State, 926), false));
            WriteLiteralTo(__razor_helper_writer, ">\r\n");
#line 30 "LogPage.cshtml"
                

#line default
#line hidden

#line 30 "LogPage.cshtml"
                 for (var i = 0; i < level; i++)
                {

#line default
#line hidden

            WriteLiteralTo(__razor_helper_writer, "                    <span class=\"tab\"></span>\r\n");
#line 33 "LogPage.cshtml"
                }

#line default
#line hidden

            WriteLiteralTo(__razor_helper_writer, "                ");
#line 34 "LogPage.cshtml"
WriteTo(__razor_helper_writer, log.State);

#line default
#line hidden
            WriteLiteralTo(__razor_helper_writer, "\r\n            </td>\r\n            <td");
            WriteAttributeTo(__razor_helper_writer, "title", Tuple.Create(" title=\"", 1137), Tuple.Create("\"", 1159), 
            Tuple.Create(Tuple.Create("", 1145), Tuple.Create<System.Object, System.Int32>(log.Exception, 1145), false));
            WriteLiteralTo(__razor_helper_writer, ">");
#line 36 "LogPage.cshtml"
         WriteTo(__razor_helper_writer, log.Exception);

#line default
#line hidden
            WriteLiteralTo(__razor_helper_writer, "</td>\r\n        </tr>\r\n");
#line 38 "LogPage.cshtml"
    }

#line default
#line hidden

        }
        );
#line 39 "LogPage.cshtml"
}

#line default
#line hidden

public  HelperResult 
#line 41 "LogPage.cshtml"
Traverse(ScopeNode node, int level)
{

#line default
#line hidden
        return new HelperResult((__razor_helper_writer) => {
#line 42 "LogPage.cshtml"
 
    // print start time
    

#line default
#line hidden

#line 44 "LogPage.cshtml"
WriteTo(__razor_helper_writer, LogRow(new LogInfo()
    {
        Name = node.Name,
        Time = node.StartTime,
        Severity = TraceType.Verbose,
        State = "Beginning " + node.State,
    }, level));

#line default
#line hidden
#line 50 "LogPage.cshtml"
             ;
    var m = 0;
    var c = 0;
    while (m < node.Messages.Count && c < node.Children.Count)
    {
        if (node.Messages[m].Time < node.Children[c].StartTime)
        {
            

#line default
#line hidden

#line 57 "LogPage.cshtml"
WriteTo(__razor_helper_writer, LogRow(node.Messages[m], level));

#line default
#line hidden
#line 57 "LogPage.cshtml"
                                            
            m++;
        }
        else
        {
            

#line default
#line hidden

#line 62 "LogPage.cshtml"
WriteTo(__razor_helper_writer, Traverse(node.Children[c], level + 1));

#line default
#line hidden
#line 62 "LogPage.cshtml"
                                                  
            c++;
        }
    }
    if (m < node.Messages.Count)
    {
        for (var i = m; i < node.Messages.Count; i++)
        {
            

#line default
#line hidden

#line 70 "LogPage.cshtml"
WriteTo(__razor_helper_writer, LogRow(node.Messages[i], level));

#line default
#line hidden
#line 70 "LogPage.cshtml"
                                            
        }
    }
    else
    {
        for (var i = c; i < node.Children.Count; i++)
        {
            

#line default
#line hidden

#line 77 "LogPage.cshtml"
WriteTo(__razor_helper_writer, Traverse(node.Children[i], level + 1));

#line default
#line hidden
#line 77 "LogPage.cshtml"
                                                  
        }
    }
    // print end time
    

#line default
#line hidden

#line 81 "LogPage.cshtml"
WriteTo(__razor_helper_writer, LogRow(new LogInfo()
    {
        Name = node.Name,
        Time = node.EndTime,
        Severity = TraceType.Verbose,
        State = string.Format("Completed {0} in {1}ms", node.State, node.EndTime - node.StartTime)
    }, level));

#line default
#line hidden
#line 87 "LogPage.cshtml"
             ;

#line default
#line hidden

        }
        );
#line 88 "LogPage.cshtml"
}

#line default
#line hidden

#line 10 "LogPage.cshtml"

    public LogPage(LogPageModel model)
    {
        Model = model;
    }

    public LogPageModel Model { get; set; }

#line default
#line hidden
        #line hidden
        public LogPage()
        {
        }

        #pragma warning disable 1998
        public override async Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n\r\n");
            WriteLiteral("\r\n");
            WriteLiteral(@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"" />
    <title>ELM</title>
    <script src=""//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js""></script>
    <style>
        body {
    font-family: 'Segoe UI', Tahoma, Arial, Helvetica, sans-serif;
    font-size: .813em;
    line-height: 1.4em;
    white-space: nowrap;
    margin: 10px;
}

col:nth-child(2) {
    background-color: #FAFAFA;
}

h1 {
    font-family: Arial, Helvetica, sans-serif;
    font-size: 2em;
}

table {
    margin: 0px auto;
    border-spacing: 0px;
    table-layout: fixed;
    width: 100%; 
    border-collapse: collapse;
}

td {
    text-overflow: ellipsis;
    overflow: hidden;
}

td, th {
    padding: 4px;
}

thead {
    font-size: 1em;
    font-family: Arial;
}

tr {
    height: 23px;
}

tr:nth-child(2n) {
    background-color: #F6F6F6;
}

#requestHeader {
    border-bottom: solid 1px gray;
    border-top: solid 1px gray;
    margin-bottom: 2px;
    font-size: 1em;
    line-height: 2em;
}

.date, .time {
    width: 70px; 
}

.logHeader {
    border-bottom: 1px solid lightgray;
    color: gray;
    text-align: left;
}

.logState {
    text-overflow: ellipsis;
    overflow: hidden;
}

.logTd {
    border-left: 1px solid gray;
    padding: 0px;
}

.logs {
    width: 80%;
}

.requestRow>td {
    border-bottom: solid 1px gray;
}

.severity {
    width: 80px;
}

.tab {
    margin-left: 30px;
}

.Critical {
    background-color: red;
    color: white;
}

.Error {
    color: red;
}

.Information {
    color: blue;
}

.Verbose {
    color: black;
}

.Warning {
    color: orange;
}
    </style>
</head>
<body>
    <h1>ELM</h1>
    <form method=""get"">
        <select name=""level"">
");
#line 104 "LogPage.cshtml"
            

#line default
#line hidden

#line 104 "LogPage.cshtml"
             foreach (var severity in Enum.GetValues(typeof(TraceType)))
            {
                var s = (int)severity;
                if ((int)Model.Options.MinLevel == s)
                {

#line default
#line hidden

            WriteLiteral("                    <option");
            WriteAttribute("value", Tuple.Create(" value=\"", 2999), Tuple.Create("\"", 3009), 
            Tuple.Create(Tuple.Create("", 3007), Tuple.Create<System.Object, System.Int32>(s, 3007), false));
            WriteLiteral(" selected=\"selected\">");
#line 109 "LogPage.cshtml"
                                                      Write(severity);

#line default
#line hidden
            WriteLiteral("</option>\r\n");
#line 110 "LogPage.cshtml"
                }
                else
                {

#line default
#line hidden

            WriteLiteral("                    <option");
            WriteAttribute("value", Tuple.Create(" value=\"", 3138), Tuple.Create("\"", 3148), 
            Tuple.Create(Tuple.Create("", 3146), Tuple.Create<System.Object, System.Int32>(s, 3146), false));
            WriteLiteral(">");
#line 113 "LogPage.cshtml"
                                  Write(severity);

#line default
#line hidden
            WriteLiteral("</option>\r\n");
#line 114 "LogPage.cshtml"
                }
            }

#line default
#line hidden

            WriteLiteral("        </select>\r\n        <input type=\"text\" name=\"name\"");
            WriteAttribute("value", Tuple.Create(" value=\"", 3261), Tuple.Create("\"", 3294), 
            Tuple.Create(Tuple.Create("", 3269), Tuple.Create<System.Object, System.Int32>(Model.Options.NamePrefix, 3269), false));
            WriteLiteral(@" />
        <input type=""submit"" value=""filter"" />
    </form>

    <table id=""requestTable"">
        <thead id=""requestHeader"">
            <tr>
                <th class=""path"">Path</th>
                <th class=""host"">Host</th>
                <th class=""statusCode"">Status Code</th>
                <th class=""logs"">Logs</th>
            </tr>
        </thead>
        <colgroup>
            <col />
            <col />
            <col />
            <col />
        </colgroup>
");
#line 136 "LogPage.cshtml"
        

#line default
#line hidden

#line 136 "LogPage.cshtml"
         foreach (var activity in Model.LogTree.Reverse())
        {

#line default
#line hidden

            WriteLiteral("            <tbody>\r\n                <tr class=\"requestRow\">\r\n");
#line 140 "LogPage.cshtml"
                    

#line default
#line hidden

#line 140 "LogPage.cshtml"
                      
                        if (activity.Key.HttpInfo != null)
                        {
                            var requestPath = Model.Path.Value + "/" + activity.Key.HttpInfo.RequestID;

#line default
#line hidden

            WriteLiteral("                            <td><a");
            WriteAttribute("href", Tuple.Create(" href=\"", 4182), Tuple.Create("\"", 4201), 
            Tuple.Create(Tuple.Create("", 4189), Tuple.Create<System.Object, System.Int32>(requestPath, 4189), false));
            WriteAttribute("title", Tuple.Create(" title=\"", 4202), Tuple.Create("\"", 4237), 
            Tuple.Create(Tuple.Create("", 4210), Tuple.Create<System.Object, System.Int32>(activity.Key.HttpInfo.Path, 4210), false));
            WriteLiteral(">");
#line 144 "LogPage.cshtml"
                                                                                      Write(activity.Key.HttpInfo.Path);

#line default
#line hidden
            WriteLiteral("</a></td>\r\n                            <td>");
#line 145 "LogPage.cshtml"
                           Write(activity.Key.HttpInfo.Host);

#line default
#line hidden
            WriteLiteral("</td>\r\n                            <td>");
#line 146 "LogPage.cshtml"
                           Write(activity.Key.HttpInfo.StatusCode);

#line default
#line hidden
            WriteLiteral("</td>\r\n");
#line 147 "LogPage.cshtml"
                        }
                        else
                        {

#line default
#line hidden

            WriteLiteral("                            <td colspan=\"3\">BOO</td>\r\n");
#line 151 "LogPage.cshtml"
                        }
                    

#line default
#line hidden

            WriteLiteral(@"
                    <td class=""logTd"">
                        <table class=""logTable"">
                            <thead class=""logHeader"">
                                <tr>
                                    <th class=""date"">Date</th>
                                    <th class=""time"">Time</th>
                                    <th class=""name"">Name</th>
                                    <th class=""severity"">Severity</th>
                                    <th class=""state"">State</th>
                                    <th class=""error"">Error</th>
                                </tr>
                            </thead>
                            <tbody>
                                ");
#line 166 "LogPage.cshtml"
                           Write(Traverse(activity.Value, 0));

#line default
#line hidden
            WriteLiteral("\r\n                            </tbody>\r\n                        </table>\r\n       " +
"             </td>\r\n                </tr>\r\n            </tbody>\r\n");
#line 172 "LogPage.cshtml"
        }

#line default
#line hidden

            WriteLiteral("    </table>\r\n</body>\r\n</html>");
        }
        #pragma warning restore 1998
    }
}
