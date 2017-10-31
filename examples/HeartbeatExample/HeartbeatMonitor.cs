using System.Threading.Tasks;
using Collector.Common.Heartbeat;
using Microsoft.Extensions.Logging;

namespace HeartbeatExample
{
    public class HeartbeatMonitor : IHeartbeatMonitor
    {
        private readonly ILogger _logger;

        public HeartbeatMonitor(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger<HeartbeatMonitor>();
        }
        public Task RunAsync()
        {
            _logger.LogInformation("Doint stuff");
            return Task.CompletedTask;
        }
    }
}
