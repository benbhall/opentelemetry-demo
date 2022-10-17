using Microsoft.AspNetCore.Mvc;
using OpenTelemetry;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OpenTelemetryDemo.ServiceA.Controllers
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
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Fun times in service A");

            using var source = new ActivitySource("ExampleTracer");

            // A span
            using var activity = source.StartActivity("Call to Service B");

            Baggage.SetBaggage("ExampleItem", "The information");

            // 'Ping' Service B
            using var client = new HttpClient();
            _ = await client.GetAsync("http://aspcore-service-b:6001/ping");

            // Another span
            using var activityTwo = source.StartActivity("Arbitrary 10ms delay");
            await Task.Delay(10);

            return Ok();
        }
    }
}