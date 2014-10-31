using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Diagnostics.Elm;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace ElmSampleApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IElmStore, ElmStore>(); // registering the service so it can be injected into constructors
            services.Configure<ElmOptions>(options =>
            {
                options.Path = new PathString("/foo");
            });
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory factory)
        {
            app.UseElm();
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello world");
                throw new InvalidOperationException();
            });
        }
    }
}
