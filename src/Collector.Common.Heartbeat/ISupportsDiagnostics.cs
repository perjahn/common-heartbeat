using System.Threading.Tasks;

namespace Collector.Common.Heartbeat
{
    public interface ISupportsDiagnostics
    {
        /// <summary>
        /// Check the health of the component.
        /// </summary>
        /// <returns>A task that represents the execution of this middleware.</returns>
        Task PerformHealthCheckAsync();
    }
}