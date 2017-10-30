using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace Collector.Common.Heartbeat
{
    public static class HeartbeatExtensions
    {
        /// <summary>
        /// Will register a heartbeat route endpoint and run health check
        /// </summary>s
        /// <param name="applicationBuilder">The <see cref="T:Microsoft.AspNetCore.Builder.IApplicationBuilder" /></param>
        /// <param name="options">Options for heartbeat.</param>
        public static IApplicationBuilder UseHeartbeat(this IApplicationBuilder applicationBuilder, HeartbeatOptions options = null)
        {
            return applicationBuilder.UseHeartbeat<IHeartbeatMonitor>(monitor => monitor.RunHealthCheckAsync(), options);
        }

        /// <summary>
        /// Will register a heartbeat route endpoint and run health check
        /// </summary>
        /// <typeparam name="T">Type of the health monitor</typeparam>
        /// <param name="applicationBuilder">The <see cref="T:Microsoft.AspNetCore.Builder.IApplicationBuilder" /></param>
        /// <param name="options">Options for heartbeat.</param>
        /// <param name="healthCheckFunc">Function to execute on <see cref="T"/></param>
        public static IApplicationBuilder UseHeartbeat<T>(this IApplicationBuilder applicationBuilder,
            Func<T, Task> healthCheckFunc, HeartbeatOptions options = null)
        {
            if (applicationBuilder == null)
            {
                throw new ArgumentNullException(nameof(applicationBuilder));
            }

            options = options ?? new HeartbeatOptions();

            if (string.IsNullOrWhiteSpace(options.HeartbeatRoute))
                throw new ArgumentNullException(nameof(options.HeartbeatRoute));
            if (!string.IsNullOrWhiteSpace(options.ApiKey) && string.IsNullOrWhiteSpace(options.ApiKeyHeaderKey))
                throw new ArgumentNullException(nameof(options.ApiKeyHeaderKey));

            return applicationBuilder.Map(options.HeartbeatRoute, app =>
            {
                app.UseMiddleware<HeartbeatMiddleware<T>>(options, healthCheckFunc);
            });

        }
    }
}
