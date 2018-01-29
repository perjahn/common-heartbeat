using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collector.Common.Heartbeat;

namespace HeartbeatExample
{
    public class SampleComponent : ISupportsDiagnostics

    {
        private int _i = 0;

        public async Task PerformHealthCheckAsync()
        {
            await Task.Delay(500);
            _i++;
            if (_i % 3 == 0)
            {
                throw new Exception("Internal error");
            }
            await Task.Delay(250);
        }
    }
}
