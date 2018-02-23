using System;
using System.Threading.Tasks;
using Collector.Common.Heartbeat;
using Microsoft.Extensions.Logging;

namespace HeartbeatExample
{
    public class HeartbeatMonitor : IHeartbeatMonitor
    {
        private readonly ILogger _logger;
        private SampleComponent _component = new SampleComponent();
        
        public HeartbeatMonitor(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger<HeartbeatMonitor>();
        }
        public Task<DiagnosticsResults> RunAsync()
        {
            _logger.LogInformation("Running diagnostics tests");
            return DiagnosticsHelper.RunDiagnosticsTests(new [] { (Func<Task>)_component.PerformHealthCheckAsync, LocalTestFunc }, parallel:true);
        }

        private Task LocalTestFunc()
        {
            return Task.CompletedTask;
        }
    }
}
