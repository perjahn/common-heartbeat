using System.Threading.Tasks;

namespace Collector.Common.Heartbeat
{
    public interface ISupportsDiagnostics
    {
        /// <summary>
        /// Check the healthy of the component/service. Should throw exception if check fails.
        /// </summary>
        /// <returns></returns>
        Task PerformHealthCheckAsync();
    }
}