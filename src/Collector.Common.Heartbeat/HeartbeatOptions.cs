namespace Collector.Common.Heartbeat
{
    public class HeartbeatOptions
    {
        public string ApiKey { get; set; }
        public string ApiKeyHeaderKey { get; set; } = "DiagnosticsAPIKey";
        public string HeartbeatRoute { get; set; } = "/api/heartbeat";
    }
}