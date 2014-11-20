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
            WriteLiteral(@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"" />
    <title>ASP.NET Logs</title>
    <script src=""http://ajax.aspnetcdn.com/ajax/jquery/jquery-2.1.1.min.js""></script>
    <style>
        body {
    font-family: 'Segoe UI', Tahoma, Arial, Helvtica, sans-serif;
    line-height: 1.4em;
}

h1 {
    font-family: 'Segoe UI', Helvetica, sans-serif;
    font-size: 2.5em;
}

td {
    text-overflow: ellipsis;
    overflow: hidden;
}

tr:nth-child(2n) {
    background-color: #F6F6F6;
}

.critical {
    background-color: red;
    color: white;
}

.error {
    color: red;
}

.information {
    color: blue;
}

.verbose {
    color: black;
}

.warning {
    color: orange;
}
        body {
    font-size: 0.9em;
    width: 90%;
    margin: 0px auto;
}

h1 {
    padding-bottom: 10px;
}

h2 {
    font-weight: normal;
}

table {
    border-spacing: 0px;
    width: 100%;
    border-collapse: collapse;
    border: 1px solid black;
    white-space: pre-wrap;
}

th {
    font-family: Arial;
}

td, th {
    padding: 8px;
}

#headerTable, #cookieTable {
    border: none;
    height: 100%;
}

#headerTd {
    white-space: normal;
}

#label {
    width: 20%;
    border-right: 1px solid black;
}

#logs{
    margin-top: 10px;
    margin-bottom: 20px;
}

#logs>tbody>tr>td {
    border-right: 1px dashed lightgray;
}

#logs>thead>tr>th {
    border: 1px solid black;
}
    </style>
</head>
<body>
    <h1>ASP.NET Logs</h1>
");
#line 30 "RequestPage.cshtml"
    

#line default
#line hidden

#line 30 "RequestPage.cshtml"
      
        var context = Model.Logs.FirstOrDefault()?.ActivityContext?.HttpInfo;
    

#line default
#line hidden

            WriteLiteral("\r\n");
#line 33 "RequestPage.cshtml"
    

#line default
#line hidden

#line 33 "RequestPage.cshtml"
     if (context != null)
    {

#line default
#line hidden

            WriteLiteral("        <h2 id=\"requestHeader\">Request Details</h2>\r\n        <table id=\"requestDe" +
"tails\">\r\n            <colgroup><col id=\"label\" /><col /></colgroup>\r\n\r\n         " +
"   <tr>\r\n                <th>Path</th>\r\n                <td>");
#line 41 "RequestPage.cshtml"
               Write(context.Path);

#line default
#line hidden
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Host</th>\r\n      " +
"          <td>");
#line 45 "RequestPage.cshtml"
               Write(context.Host);

#line default
#line hidden
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Content Type</th>" +
"\r\n                <td>");
#line 49 "RequestPage.cshtml"
               Write(context.ContentType);

#line default
#line hidden
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Method</th>\r\n    " +
"            <td>");
#line 53 "RequestPage.cshtml"
               Write(context.Method);

#line default
#line hidden
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Protocol</th>\r\n  " +
"              <td>");
#line 57 "RequestPage.cshtml"
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
#line 70 "RequestPage.cshtml"
                            

#line default
#line hidden

#line 70 "RequestPage.cshtml"
                             foreach (var header in context.Headers)
                            {

#line default
#line hidden

            WriteLiteral("                                <tr>\r\n                                    <td>");
#line 73 "RequestPage.cshtml"
                                   Write(header.Key);

#line default
#line hidden
            WriteLiteral("</td>\r\n                                    <td>");
#line 74 "RequestPage.cshtml"
                                   Write(string.Join(";", header.Value));

#line default
#line hidden
            WriteLiteral("</td>\r\n                                </tr>\r\n");
#line 76 "RequestPage.cshtml"
                            }

#line default
#line hidden

            WriteLiteral("                        </tbody>\r\n                    </table>\r\n                <" +
"/td>\r\n            </tr>\r\n            <tr>\r\n                <th>Status Code</th>\r" +
"\n                <td>");
#line 83 "RequestPage.cshtml"
               Write(context.StatusCode);

#line default
#line hidden
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>User</th>\r\n      " +
"          <td>");
#line 87 "RequestPage.cshtml"
               Write(context.User.Identity.Name);

#line default
#line hidden
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Claims</th>\r\n    " +
"            <td>\r\n");
#line 92 "RequestPage.cshtml"
                    

#line default
#line hidden

#line 92 "RequestPage.cshtml"
                     if (context.User.Claims.Any())
                    {

#line default
#line hidden

            WriteLiteral(@"                        <table id=""claimsTable"">
                            <thead>
                                <tr>
                                    <th>Issuer</th>
                                    <th>Value</th>
                                </tr>
                            </thead>
                            <tbody>
");
#line 102 "RequestPage.cshtml"
                                

#line default
#line hidden

#line 102 "RequestPage.cshtml"
                                 foreach (var claim in context.User.Claims)
                                {

#line default
#line hidden

            WriteLiteral("                                    <tr>\r\n                                       " +
" <td>");
#line 105 "RequestPage.cshtml"
                                       Write(claim.Issuer);

#line default
#line hidden
            WriteLiteral("</td>\r\n                                        <td>");
#line 106 "RequestPage.cshtml"
                                       Write(claim.Value);

#line default
#line hidden
            WriteLiteral("</td>\r\n                                    </tr>\r\n");
#line 108 "RequestPage.cshtml"
                                }

#line default
#line hidden

            WriteLiteral("                            </tbody>\r\n                        </table>\r\n");
#line 111 "RequestPage.cshtml"
                    }

#line default
#line hidden

            WriteLiteral("                </td>\r\n            </tr>\r\n            <tr>\r\n                <th>S" +
"cheme</th>\r\n                <td>");
#line 116 "RequestPage.cshtml"
               Write(context.Scheme);

#line default
#line hidden
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Query</th>\r\n     " +
"           <td>");
#line 120 "RequestPage.cshtml"
               Write(context.Query.Value);

#line default
#line hidden
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Cookies</th>\r\n   " +
"             <td>\r\n");
#line 125 "RequestPage.cshtml"
                    

#line default
#line hidden

#line 125 "RequestPage.cshtml"
                     if (context.Cookies.Any())
                    {

#line default
#line hidden

            WriteLiteral(@"                        <table id=""cookieTable"">
                            <thead>
                                <tr>
                                    <th>Variable</th>
                                    <th>Value</th>
                                </tr>
                            </thead>
                            <tbody>
");
#line 135 "RequestPage.cshtml"
                                

#line default
#line hidden

#line 135 "RequestPage.cshtml"
                                 foreach (var cookie in context.Cookies)
                                {

#line default
#line hidden

            WriteLiteral("                                    <tr>\r\n                                       " +
" <td>");
#line 138 "RequestPage.cshtml"
                                       Write(cookie.Key);

#line default
#line hidden
            WriteLiteral("</td>\r\n                                        <td>");
#line 139 "RequestPage.cshtml"
                                       Write(string.Join(";", cookie.Value));

#line default
#line hidden
            WriteLiteral("</td>\r\n                                    </tr>\r\n");
#line 141 "RequestPage.cshtml"
                                }

#line default
#line hidden

            WriteLiteral("                            </tbody>\r\n                        </table>\r\n");
#line 144 "RequestPage.cshtml"
                    }

#line default
#line hidden

            WriteLiteral("                </td>\r\n            </tr>\r\n        </table>\r\n");
#line 148 "RequestPage.cshtml"
    }

#line default
#line hidden

            WriteLiteral("    <h2>Logs</h2>\r\n    <form method=\"get\">\r\n        <select name=\"level\">\r\n");
#line 152 "RequestPage.cshtml"
            

#line default
#line hidden

#line 152 "RequestPage.cshtml"
             foreach (var severity in Enum.GetValues(typeof(LogLevel)))
            {
                var severityInt = (int)severity;
                if ((int)Model.Options.MinLevel == severityInt)
                {

#line default
#line hidden

            WriteLiteral("                    <option");
            WriteAttribute("value", Tuple.Create(" value=\"", 5187), Tuple.Create("\"", 5207), 
            Tuple.Create(Tuple.Create("", 5195), Tuple.Create<System.Object, System.Int32>(severityInt, 5195), false));
            WriteLiteral(" selected=\"selected\">");
#line 157 "RequestPage.cshtml"
                                                                Write(severity);

#line default
#line hidden
            WriteLiteral("</option>\r\n");
#line 158 "RequestPage.cshtml"
                }
                else
                {

#line default
#line hidden

            WriteLiteral("                    <option");
            WriteAttribute("value", Tuple.Create(" value=\"", 5336), Tuple.Create("\"", 5356), 
            Tuple.Create(Tuple.Create("", 5344), Tuple.Create<System.Object, System.Int32>(severityInt, 5344), false));
            WriteLiteral(">");
#line 161 "RequestPage.cshtml"
                                            Write(severity);

#line default
#line hidden
            WriteLiteral("</option>\r\n");
#line 162 "RequestPage.cshtml"
                }
            }

#line default
#line hidden

            WriteLiteral("        </select>\r\n        <input type=\"text\" name=\"name\"");
            WriteAttribute("value", Tuple.Create(" value=\"", 5469), Tuple.Create("\"", 5502), 
            Tuple.Create(Tuple.Create("", 5477), Tuple.Create<System.Object, System.Int32>(Model.Options.NamePrefix, 5477), false));
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
#line 179 "RequestPage.cshtml"
        

#line default
#line hidden

#line 179 "RequestPage.cshtml"
         foreach (var log in Model.Logs)
        {

#line default
#line hidden

            WriteLiteral("            <tr>\r\n                <td>");
#line 182 "RequestPage.cshtml"
               Write(string.Format("{0:MM/dd/yy}", log.Time));

#line default
#line hidden
            WriteLiteral("</td>\r\n                <td>");
#line 183 "RequestPage.cshtml"
               Write(string.Format("{0:H:mm:ss}", log.Time));

#line default
#line hidden
            WriteLiteral("</td>\r\n                <td");
            WriteAttribute("class", Tuple.Create(" class=\"", 6079), Tuple.Create("\"", 6130), 
            Tuple.Create(Tuple.Create("", 6087), Tuple.Create<System.Object, System.Int32>(log.Severity.ToString().ToLowerInvariant(), 6087), false));
            WriteLiteral(">");
#line 184 "RequestPage.cshtml"
                                                                   Write(log.Severity);

#line default
#line hidden
            WriteLiteral("</td>\r\n                <td");
            WriteAttribute("title", Tuple.Create(" title=\"", 6171), Tuple.Create("\"", 6188), 
            Tuple.Create(Tuple.Create("", 6179), Tuple.Create<System.Object, System.Int32>(log.Name, 6179), false));
            WriteLiteral(">");
#line 185 "RequestPage.cshtml"
                                 Write(log.Name);

#line default
#line hidden
            WriteLiteral("</td>\r\n                <td");
            WriteAttribute("title", Tuple.Create(" title=\"", 6225), Tuple.Create("\"", 6245), 
            Tuple.Create(Tuple.Create("", 6233), Tuple.Create<System.Object, System.Int32>(log.Message, 6233), false));
            WriteLiteral(" class=\"logState\" width=\"100px\">");
#line 186 "RequestPage.cshtml"
                                                                   Write(log.Message);

#line default
#line hidden
            WriteLiteral("</td>\r\n                <td");
            WriteAttribute("title", Tuple.Create(" title=\"", 6316), Tuple.Create("\"", 6338), 
            Tuple.Create(Tuple.Create("", 6324), Tuple.Create<System.Object, System.Int32>(log.Exception, 6324), false));
            WriteLiteral(">");
#line 187 "RequestPage.cshtml"
                                      Write(log.Exception);

#line default
#line hidden
            WriteLiteral("</td>\r\n            </tr>\r\n");
#line 189 "RequestPage.cshtml"
        }

#line default
#line hidden

            WriteLiteral("    </table>\r\n    <script type=\"text/javascript\">\r\n        $(document).ready(func" +
"tion () {\r\n            $(\"#requestHeader\").click(function () {\r\n                " +
"$(\"#requestDetails\").toggle();\r\n            });\r\n        });\r\n    </script>\r\n</b" +
"ody>\r\n</html>");
        }
        #pragma warning restore 1998
    }
}
