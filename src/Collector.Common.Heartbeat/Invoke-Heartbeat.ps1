$args = @{
    Uri = "http://localhost:64251/api/heartbeat"
    Method = "Get"
    Headers = @{ "DiagnosticsAPIKey"="Secret" }
}

Invoke-RestMethod @args -UseBasicParsing

