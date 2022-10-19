# .NET OpenTelemetry Demo

If you've landed here, you've probably just listened to me speak. I hope it wasn't too boring!

The accompanying blog article series on [The Modern Observability Problem and OpenTelemetry](https://failingfast.io/opentelemetry-observability/).

![Simple .NET OpenTelemetry Diagram](/docs/dotnet1.png)

This is a small demo consisting 4 Docker containers:

- 2 x .NET 6 web apps
- An OpenTelemetry Collector
- Jaeger

And a Application Insights instance in Azure.

## Prerequisites and setup

You will need an Azure subscription. You can get a [free account here](https://azure.microsoft.com/en-gb/free/).

1. In your Azure subscription, create a new Application Insights instance.

2. Add the instrumentation key from that instance to `otel-collector-config.yml` :

```yaml
exporters:
  azuremonitor:
    instrumentation_key: <paste key here>
```

3. Add the full Application Insights connection string to `appsettings.json` in Service B:

```json
  "ApplicationInsights": {
    "ConnectionString": <paste connection string here>
  }  
```

## Running the demo

Build and start the containers using `start.bat`.

When the containers are running, visit the URL `http://localhost:5001/ping`. This will call Service B on `http://localhost:6001/ping` to demonstrate the end-to-end telemetry.

Some calls Service B are set to throw exceptions to generate examples if errors also.
