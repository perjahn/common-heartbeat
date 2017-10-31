# common-heartbeat
Heartbeat middleware

Intercepts HttpGet requests to /api/heartbeat and executes a registered heartbeatmonitor that runs healthchecks on the system.
By default it will try to find a registered instance of IHeartbeatMonitor and execute 


Basic usage

```csharp
using Collector.Common.Heartbeat

app.UseHeartbeat();
//HeartbeatOptions Default values:
//ApiKey = null
//ApiKeyHeaderKey = "DiagnosticsAPIKey"
//HeartbeatRoute = "/api/heartbeat"
```

Advanced usage
```csharp
using Collector.Common.Heartbeat

app.UseHeartbeat<IHeartbeatMonitor>(
	monitor => monitor.RunAsync(), 
    new HeartbeatOptions {
		ApiKey = "Secret",
		ApiKeyHeaderKey = "ApiKeyAuthorization",
		HeartbeatRoute = "/api/custom/heartbeat"
	}
);
```
