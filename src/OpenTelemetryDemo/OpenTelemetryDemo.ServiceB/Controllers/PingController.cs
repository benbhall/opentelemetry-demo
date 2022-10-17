using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using System;
using System.Diagnostics;

namespace OpenTelemetryDemo.ServiceB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : ControllerBase
    {
        private readonly ILogger _logger;

        public PingController(ILogger<PingController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Get()
        {

            _logger.LogInformation("Now inside service B");

            var infoFromContext = Baggage.Current.GetBaggage("ExampleItem");

            using var source = new ActivitySource("ExampleTracer");

            // A span
            using var activity = source.StartActivity("In Service B GET method");
            activity?.SetTag("InfoServiceBReceived", infoFromContext);

            if (DateTime.Now.Second % 2 == 0) {
                  _logger.LogError("failing log");
                throw new Exception("Failing...");
            }

            return Ok();
        }
    }
}