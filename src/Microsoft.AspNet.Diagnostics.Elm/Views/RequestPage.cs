namespace Microsoft.AspNet.Diagnostics.Elm.Views
{
#line 1 "RequestPage.cshtml"
using System

#line default
#line hidden
    ;
#line 2 "RequestPage.cshtml"
using System.Globalization

#line default
#line hidden
    ;
#line 3 "RequestPage.cshtml"
using System.Linq

#line default
#line hidden
    ;
#line 4 "RequestPage.cshtml"
using Microsoft.AspNet.Diagnostics.Elm

#line default
#line hidden
    ;
#line 5 "RequestPage.cshtml"
using Microsoft.AspNet.Diagnostics.Elm.Views

#line default
#line hidden
    ;
#line 6 "RequestPage.cshtml"
using Microsoft.Framework.Logging

#line default
#line hidden
    ;
    using System.Threading.Tasks;

    public class RequestPage : Microsoft.AspNet.Diagnostics.Views.BaseView
    {
#line 9 "RequestPage.cshtml"

    public RequestPage(RequestPageModel model)
    {
        Model = model;
    }

    public RequestPageModel Model { get; set; }

#line default
#line hidden
        #line hidden
        public RequestPage()
        {
        }

        #pragma warning disable 1998
        public override async Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <meta charset=\"utf-8\" />\r\n    <title>ELM</ti" +
"tle>\r\n    <style>\r\n        body {\r\n    font-family: 'Segoe UI', Tahoma, Arial, Helvtica, sans-serif;\r\n    line-height: 1.4em;\r\n}\r\n\r\ntd {\r\n    text-overflow: ellipsis;\r\n    overflow: hidden;\r\n}\r\n\r\ntr:nth-child(2n) {\r\n    background-color: #F6F6F6;\r\n}\r\n\r\n.critical {\r\n    background-color: red;\r\n    color: white;\r\n}\r\n\r\n.error {\r\n    color: red;\r\n}\r\n\r\n.information {\r\n    color: blue;\r\n}\r\n\r\n.verbose {\r\n    color: black;\r\n}\r\n\r\n.warning {\r\n    color: orange;\r\n}\r\n        body {\r\n    font-size: 0.9em;\r\n    width: 90%;\r\n    margin: 0px auto;\r\n}\r\n\r\nh1, h2 {\r\n    font-weight: normal;\r\n}\r\n\r\ntable {\r\n    border-spacing: 0px;\r\n    width: 100%;\r\n    border-collapse: collapse;\r\n    border: 1px solid black;\r\n    white-space: pre-wrap;\r\n}\r\n\r\nth {\r\n    font-family: Arial;\r\n}\r\n\r\ntd, th {\r\n    padding: 8px;\r\n}\r\n\r\n#headerTable {\r\n    border: none;\r\n    height: 100%;\r\n}\r\n\r\n#headerTd {\r\n    white-space: normal;\r\n}\r\n\r\n#label {\r\n    width: 20%;\r\n    border-right: 1px solid black;\r\n}\r\n\r\n#logs>tbody>tr>td {\r\n    border-right: 1px dashed lightgray;\r\n}\r\n\r\n#logs>thead>tr>th {\r\n    border: 1px solid black;\r\n}\r\n    </style>\r\n</head>\r\n<body>\r\n    <h1>ELM</h1>\r\n");
#line 29 "RequestPage.cshtml"
    

#line default
#line hidden

#line 29 "RequestPage.cshtml"
      
        var context = Model.Logs.FirstOrDefault()?.ActivityContext?.HttpInfo;
    

#line default
#line hidden

            WriteLiteral("\r\n");
#line 32 "RequestPage.cshtml"
    

#line default
#line hidden

#line 32 "RequestPage.cshtml"
     if (context != null)
    {

#line default
#line hidden

            WriteLiteral("        <h2>Request Details</h2>\r\n        <table id=\"requestDetails\">\r\n          " +
"  <colgroup><col id=\"label\" /><col /></colgroup>\r\n\r\n            <tr>\r\n          " +
"      <th>Path</th>\r\n                <td>");
#line 40 "RequestPage.cshtml"
               Write(context.Path);

#line default
#line hidden
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Host</th>\r\n      " +
"          <td>");
#line 44 "RequestPage.cshtml"
               Write(context.Host);

#line default
#line hidden
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Content Type</th>" +
"\r\n                <td>");
#line 48 "RequestPage.cshtml"
               Write(context.ContentType);

#line default
#line hidden
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Method</th>\r\n    " +
"            <td>");
#line 52 "RequestPage.cshtml"
               Write(context.Method);

#line default
#line hidden
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Protocol</th>\r\n  " +
"              <td>");
#line 56 "RequestPage.cshtml"
               Write(context.Protocol);

#line default
#line hidden
            WriteLiteral(@"</td>
            </tr>
            <tr>
                <th>Headers</th>
                <td id=""headerTd"">
                    <table id=""headerTable"">
                        <thead>
                            <tr>
                                <th>Variable</th>
                                <th>Value</th>
                            </tr>
                        </thead>
                        <tbody>
");
#line 69 "RequestPage.cshtml"
                            

#line default
#line hidden

#line 69 "RequestPage.cshtml"
                             foreach (var header in context.Headers)
                            {

#line default
#line hidden

            WriteLiteral("                                <tr>\r\n                                    <td>");
#line 72 "RequestPage.cshtml"
                                   Write(header.Key);

#line default
#line hidden
            WriteLiteral("</td>\r\n                                    <td>");
#line 73 "RequestPage.cshtml"
                                   Write(string.Join(";", header.Value));

#line default
#line hidden
            WriteLiteral("</td>\r\n                                </tr>\r\n");
#line 75 "RequestPage.cshtml"
                            }

#line default
#line hidden

            WriteLiteral("                        </tbody>\r\n                    </table>\r\n                <" +
"/td>\r\n            </tr>\r\n            <tr>\r\n                <th>Status Code</th>\r" +
"\n                <td>");
#line 82 "RequestPage.cshtml"
               Write(context.StatusCode);

#line default
#line hidden
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>User</th>\r\n      " +
"          \r\n                <td>");
#line 87 "RequestPage.cshtml"
               Write(context.User.Identity.Name);

#line default
#line hidden
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Scheme</th>\r\n    " +
"            <td>");
#line 91 "RequestPage.cshtml"
               Write(context.Scheme);

#line default
#line hidden
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Query</th>\r\n     " +
"           <td>");
#line 95 "RequestPage.cshtml"
               Write(context.Query.Value);

#line default
#line hidden
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Cookies</th>\r\n   " +
"             <td>\r\n");
#line 100 "RequestPage.cshtml"
                    

#line default
#line hidden

#line 100 "RequestPage.cshtml"
                     if (context.Cookies.Any())
                    {

#line default
#line hidden

            WriteLiteral(@"                        <table id=""queryTable"">
                            <thead>
                                <tr>
                                    <th>Variable</th>
                                    <th>Value</th>
                                </tr>
                            </thead>
                            <tbody>
");
#line 110 "RequestPage.cshtml"
                                

#line default
#line hidden

#line 110 "RequestPage.cshtml"
                                 foreach (var cookie in context.Cookies)
                                {

#line default
#line hidden

            WriteLiteral("                                    <tr>\r\n                                       " +
" <td>");
#line 113 "RequestPage.cshtml"
                                       Write(cookie.Key);

#line default
#line hidden
            WriteLiteral("</td>\r\n                                        <td>");
#line 114 "RequestPage.cshtml"
                                       Write(string.Join(";", cookie.Value));

#line default
#line hidden
            WriteLiteral("</td>\r\n                                    </tr>\r\n");
#line 116 "RequestPage.cshtml"
                                }

#line default
#line hidden

            WriteLiteral("                            </tbody>\r\n                        </table>\r\n");
#line 119 "RequestPage.cshtml"
                    }

#line default
#line hidden

            WriteLiteral("                </td>\r\n            </tr>\r\n        </table>\r\n");
#line 123 "RequestPage.cshtml"
    }

#line default
#line hidden

            WriteLiteral("    <h2>Logs</h2>\r\n    <form method=\"get\">\r\n        <select name=\"level\">\r\n");
#line 127 "RequestPage.cshtml"
            

#line default
#line hidden

#line 127 "RequestPage.cshtml"
             foreach (var severity in Enum.GetValues(typeof(LogLevel)))
            {
                var severityInt = (int)severity;
                if ((int)Model.Options.MinLevel == severityInt)
                {

#line default
#line hidden

            WriteLiteral("                    <option");
            WriteAttribute("value", Tuple.Create(" value=\"", 4138), Tuple.Create("\"", 4158), 
            Tuple.Create(Tuple.Create("", 4146), Tuple.Create<System.Object, System.Int32>(severityInt, 4146), false));
            WriteLiteral(" selected=\"selected\">");
#line 132 "RequestPage.cshtml"
                                                                Write(severity);

#line default
#line hidden
            WriteLiteral("</option>\r\n");
#line 133 "RequestPage.cshtml"
                }
                else
                {

#line default
#line hidden

            WriteLiteral("                    <option");
            WriteAttribute("value", Tuple.Create(" value=\"", 4287), Tuple.Create("\"", 4307), 
            Tuple.Create(Tuple.Create("", 4295), Tuple.Create<System.Object, System.Int32>(severityInt, 4295), false));
            WriteLiteral(">");
#line 136 "RequestPage.cshtml"
                                            Write(severity);

#line default
#line hidden
            WriteLiteral("</option>\r\n");
#line 137 "RequestPage.cshtml"
                }
            }

#line default
#line hidden

            WriteLiteral("        </select>\r\n        <input type=\"text\" name=\"name\"");
            WriteAttribute("value", Tuple.Create(" value=\"", 4420), Tuple.Create("\"", 4453), 
            Tuple.Create(Tuple.Create("", 4428), Tuple.Create<System.Object, System.Int32>(Model.Options.NamePrefix, 4428), false));
            WriteLiteral(@" />
        <input type=""submit"" value=""filter"" />
    </form>
    <table id=""logs"">
        <thead>
            <tr>
                <th>Date</th>
                <th>Time</th>
                <th>Severity</th>
                <th>Name</th>
                <th>State</th>
                <th>Error</th>
            </tr>
        </thead>
");
#line 154 "RequestPage.cshtml"
        

#line default
#line hidden

#line 154 "RequestPage.cshtml"
         foreach (var log in Model.Logs)
        {

#line default
#line hidden

            WriteLiteral("            <tr>\r\n                <td>");
#line 157 "RequestPage.cshtml"
               Write(string.Format("{0:MM/dd/yy}", log.Time));

#line default
#line hidden
            WriteLiteral("</td>\r\n                <td>");
#line 158 "RequestPage.cshtml"
               Write(string.Format("{0:H:mm:ss}", log.Time));

#line default
#line hidden
            WriteLiteral("</td>\r\n                <td");
            WriteAttribute("class", Tuple.Create(" class=\"", 5030), Tuple.Create("\"", 5081), 
            Tuple.Create(Tuple.Create("", 5038), Tuple.Create<System.Object, System.Int32>(log.Severity.ToString().ToLowerInvariant(), 5038), false));
            WriteLiteral(">");
#line 159 "RequestPage.cshtml"
                                                                   Write(log.Severity);

#line default
#line hidden
            WriteLiteral("</td>\r\n                <td");
            WriteAttribute("title", Tuple.Create(" title=\"", 5122), Tuple.Create("\"", 5139), 
            Tuple.Create(Tuple.Create("", 5130), Tuple.Create<System.Object, System.Int32>(log.Name, 5130), false));
            WriteLiteral(">");
#line 160 "RequestPage.cshtml"
                                 Write(log.Name);

#line default
#line hidden
            WriteLiteral("</td>\r\n                <td");
            WriteAttribute("title", Tuple.Create(" title=\"", 5176), Tuple.Create("\"", 5194), 
            Tuple.Create(Tuple.Create("", 5184), Tuple.Create<System.Object, System.Int32>(log.State, 5184), false));
            WriteLiteral(" class=\"logState\" width=\"100px\">");
#line 161 "RequestPage.cshtml"
                                                                 Write(log.State);

#line default
#line hidden
            WriteLiteral("</td>\r\n                <td");
            WriteAttribute("title", Tuple.Create(" title=\"", 5263), Tuple.Create("\"", 5285), 
            Tuple.Create(Tuple.Create("", 5271), Tuple.Create<System.Object, System.Int32>(log.Exception, 5271), false));
            WriteLiteral(">");
#line 162 "RequestPage.cshtml"
                                      Write(log.Exception);

#line default
#line hidden
            WriteLiteral("</td>\r\n            </tr>\r\n");
#line 164 "RequestPage.cshtml"
        }

#line default
#line hidden

            WriteLiteral("    </table>\r\n</body>\r\n</html>");
        }
        #pragma warning restore 1998
    }
}
