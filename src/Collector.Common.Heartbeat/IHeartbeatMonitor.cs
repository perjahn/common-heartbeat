using System.Collections.Generic;
using System.Threading.Tasks;

namespace Collector.Common.Heartbeat
{
    public interface IHeartbeatMonitor
    {
        Task<DiagnosticsResults> RunAsync();
    }
}
