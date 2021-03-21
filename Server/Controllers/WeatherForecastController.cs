using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProtobufBlazor.Shared;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;


namespace ProtobufBlazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var rng = new Random();

            var ret = new WeatherForecastSenderArray();

            ret.Forecasts.AddRange(Enumerable.Range(1, 5).Select(index => new WeatherForecastSender
            {
                Date = Timestamp.FromDateTime(DateTime.UtcNow.AddDays(index)),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }));

            var data = ret.ToByteArray();
            return File(data, "application/octet-stream");
        }
    }
}
