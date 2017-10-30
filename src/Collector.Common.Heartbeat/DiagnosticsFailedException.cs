using System;

namespace Collector.Common.Heartbeat
{
    public class DiagnosticsFailedException : Exception
    {
        public DiagnosticsFailedException()
        {
            
        }

        public DiagnosticsFailedException(string message) : base(message)
        {
            
        }

        public DiagnosticsFailedException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}