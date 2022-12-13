using DTOs;
using Microsoft.AspNetCore.Mvc;
using System.IO.Pipes;

namespace WebApplication1.Controllers
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

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("IAsyncTest")]
        public IAsyncEnumerable<TestDTO> GetTestValue()
        {
            return _getValues();
        }

        private static readonly TestDTO[] _values = new[]
        {
            new TestDTO {IntValue= 1, StringValue = "Value 1"},
            new TestDTO {IntValue= 2, StringValue = "Value 2"},
            new TestDTO {IntValue= 3, StringValue = "Value 3"},
            new TestDTO {IntValue= 4, StringValue = "Value 4"},
            new TestDTO {IntValue= 5, StringValue = "Value 5"},
        };

        private async IAsyncEnumerable<TestDTO> _getValues()
        {
            foreach (var value in _values)
            {
                yield return value;
                await Task.Delay(TimeSpan.FromSeconds(20));
            }
        }
    }
}