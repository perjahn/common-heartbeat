using Collector.Common.Heartbeat;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HeartbeatExample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHeartbeatMonitor, HeartbeatMonitor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHeartbeat<IHeartbeatMonitor>(x => x.RunAsync(), options =>
            {
                options.ApiKey = "Secret"; // Default = string.Empty / None
                //options.ApiKeyHeaderKey = "SomeHeaderName"; // Default = "DiagnosticsAPIKey"
                //options.HeartbeatRoute = "/api/otherroute"; // Default = "/api/heartbeat"
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
