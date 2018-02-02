using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Common.Heartbeat
{
    /// <summary>
    /// Data collected from an invokation of ISupportsDiagnostics.PerformHealthCheckAsync
    /// </summary>
    public class DiagnosticsResult
    {
        public DiagnosticsResult(string componentName)
        {
            ComponentName = componentName;
            Success = true;
            ErrorMessages = new List<string>();
            DiagnosticsStartTime = DateTime.Now;
        }

        public DateTimeOffset DiagnosticsStartTime { get; set; }

        public long ElapsedMilliseconds { get; set; }

        public string ComponentName { get; set; }

        public bool Success { get; set; }

        public List<string> ErrorMessages { get; set; }
    }

    /// <summary>
    /// Aggregation of multiple DiagnosticsResult
    /// </summary>
    public class DiagnosticsResults
    {
        public DiagnosticsResults(List<DiagnosticsResult> componentResults)
        {
            ComponentResults = componentResults;
            Success = componentResults.All(component => component.Success);
            ElapsedSequentialMilliseconds = componentResults.Sum(component => component.ElapsedMilliseconds);
            ElapsedMilliseconds = ElapsedSequentialMilliseconds;
            DiagnosticsStartTime = DateTime.Now;
        }

        public List<DiagnosticsResult> ComponentResults { get; private set; }

        public bool Success { get; private set; }

        public long ElapsedSequentialMilliseconds { get; private set; }

        public long ElapsedMilliseconds { get; set; }

        public DateTimeOffset DiagnosticsStartTime { get; set; }
    }

    /// <summary>
    /// Utility class to make it easy to invoke components that implements ISupportsDiagnostics and to collect the result
    /// </summary>
    public static class DiagnosticsHelper
    {
        public static async Task<DiagnosticsResult> RunDiagnosticsTest(ISupportsDiagnostics component)
        {
            var stopWatch = Stopwatch.StartNew();
            var result = new DiagnosticsResult(component.GetType().ToString());
            try
            {
                await component.PerformHealthCheckAsync();
            }
            catch (Exception error)
            {
                result.Success = false;
                result.ErrorMessages.Add(error.ToString());
            }

            result.ElapsedMilliseconds = stopWatch.ElapsedMilliseconds;
            return result;
        }

        public static async Task<DiagnosticsResults> RunDiagnosticsTests(
            IEnumerable<ISupportsDiagnostics> components, bool parallel)
        {
            var startTime = DateTime.Now;
            var stopwatch = Stopwatch.StartNew();
            List<DiagnosticsResult> componentResults;
            if (parallel)
            {
                var testTasks = components.Select(RunDiagnosticsTest).ToList();
                await Task.WhenAll(testTasks);
                componentResults = testTasks.Select(testTask => testTask.Result).ToList();
            }
            else
            {
                var results = new List<DiagnosticsResult>();
                foreach (var component in components)
                {
                    var result = await RunDiagnosticsTest(component);
                    results.Add(result);
                }

                componentResults = results;
            }

            return new DiagnosticsResults(componentResults) { ElapsedMilliseconds = stopwatch.ElapsedMilliseconds, DiagnosticsStartTime = startTime};
        }
    }
}