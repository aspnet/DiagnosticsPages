using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace ElmSampleApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddElm(options =>
            {
                options.Path = new PathString("/foo");
                options.Filter = (name, level) => level >= LogLevel.Information;
            });
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory factory)
        {
            
            app.UseElmCapture();
            app.UseElmPage();

            var logger = factory.Create<Startup>();

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello world");
                using (logger.BeginScope("startup"))
                {
                    logger.WriteInformation("Hello world!");
                    logger.WriteError("Mort");
                }
                using (logger.BeginScope("verbose"))
                {
                    logger.WriteVerbose("some verbose stuff");
                }
                throw new InvalidOperationException();
            });
        }
    }
}
