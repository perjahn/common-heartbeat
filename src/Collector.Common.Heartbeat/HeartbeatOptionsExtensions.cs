namespace Collector.Common.Heartbeat
{
    public static class HeartbeatOptionsExtensions
    {
        public static HeartbeatOptions AddApiKey(this HeartbeatOptions options, string apiKey)
        {
            options.ApiKey = apiKey;
            return options;
        }

        public static HeartbeatOptions SetApiKeyHeaderKey(this HeartbeatOptions options, string headerKey)
        {
            options.ApiKeyHeaderKey = headerKey;
            return options;
        }

        public static HeartbeatOptions SetHeartbeatRoute(this HeartbeatOptions options, string route)
        {
            options.HeartbeatRoute = route;
            return options;
        }

    }
}