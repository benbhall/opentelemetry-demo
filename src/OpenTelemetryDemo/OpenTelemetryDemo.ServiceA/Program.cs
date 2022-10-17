
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Logs;
using System;

var builder = WebApplication.CreateBuilder(args);

// This must be set before creating a GrpcChannel/HttpClient when calling an insecure service
AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
var resourceBuilder = ResourceBuilder.CreateDefault()
    .AddService("ServiceA")
    .AddTelemetrySdk();

// Configure logging
builder.Logging.ClearProviders()
               .AddOpenTelemetry(configure =>
               {
                   configure.IncludeFormattedMessage = true;
                   configure.IncludeScopes = true;
                   configure.ParseStateValues = true;
                   configure.SetResourceBuilder(resourceBuilder)
                            .AddOtlpExporter(otlpExporterOptions =>
                            {
                                builder.Configuration.GetSection("OpenTelemetry:OtlpExporter").Bind(otlpExporterOptions);
                            });
                    // Enabled for troubleshooting
                    // .AddConsoleExporter();                        
               });

builder.Services.AddControllers();

builder.Services.AddOpenTelemetryTracing(tracerProviderBuilder =>
{
    tracerProviderBuilder.SetResourceBuilder(resourceBuilder)
        .AddSource("ExampleTracer")
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddOtlpExporter(otlpExporterOptions =>
        {
            builder.Configuration.GetSection("OpenTelemetry:OtlpExporter").Bind(otlpExporterOptions);
        });
    // Enabled for troubleshooting
    // .AddConsoleExporter();
});

var app = builder.Build();
app.UseRouting();
app.MapControllers();
app.Run();