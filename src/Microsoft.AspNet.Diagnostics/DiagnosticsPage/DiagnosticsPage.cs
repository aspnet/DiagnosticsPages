namespace Microsoft.AspNet.Diagnostics.Views
{
#line 1 "DiagnosticsPage.cshtml"
using System

#line default
#line hidden
    ;
#line 2 "DiagnosticsPage.cshtml"
using System.Globalization

#line default
#line hidden
    ;
    using System.Threading.Tasks;

    public class DiagnosticsPage : Microsoft.AspNet.Diagnostics.Views.BaseView
    {
        #line hidden
        public DiagnosticsPage()
        {
        }

        #pragma warning disable 1998
        public override async Task ExecuteAsync()
        {
#line 3 "DiagnosticsPage.cshtml"
  
    Response.ContentType = "text/html";
    string error = Request.Query["error"];
    if (!string.IsNullOrWhiteSpace(error))
    {
        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "User requested error '{0}'", error));
    }

#line default
#line hidden

            WriteLiteral("\r\n<!DOCTYPE html>\r\n\r\n<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head" +
">\r\n    <meta charset=\"utf-8\" />\r\n    <title>");
#line 16 "DiagnosticsPage.cshtml"
      Write(Resources.DiagnosticsPageHtml_Title);

#line default
#line hidden
            WriteLiteral("</title>\r\n</head>\r\n<body>\r\n    <div class=\"main\">\r\n        <h1>");
#line 20 "DiagnosticsPage.cshtml"
       Write(Resources.DiagnosticsPageHtml_Title);

#line default
#line hidden
            WriteLiteral("</h1>\r\n        <p>");
#line 21 "DiagnosticsPage.cshtml"
      Write(Resources.DiagnosticsPageHtml_Information);

#line default
#line hidden
            WriteLiteral("</p>\r\n    </div>\r\n    <div class=\"errors\">\r\n        <h2>");
#line 24 "DiagnosticsPage.cshtml"
       Write(Resources.DiagnosticsPageHtml_TestErrorSection);

#line default
#line hidden
            WriteLiteral("</h2>\r\n        <p><a");
            WriteAttribute("href", Tuple.Create(" href=\"", 763), Tuple.Create("\"", 854), 
            Tuple.Create(Tuple.Create("", 770), Tuple.Create<System.Object, System.Int32>(Request.PathBase, 770), false), 
            Tuple.Create(Tuple.Create("", 787), Tuple.Create<System.Object, System.Int32>(Request.Path, 787), false), Tuple.Create(Tuple.Create("", 800), Tuple.Create("?error=", 800), true), 
            Tuple.Create(Tuple.Create("", 807), Tuple.Create<System.Object, System.Int32>(Resources.DiagnosticsPageHtml_TestErrorMessage, 807), false));
            WriteLiteral(">throw InvalidOperationException</a></p>\r\n    </div>\r\n</body>\r\n</html>\r\n");
        }
        #pragma warning restore 1998
    }
}
