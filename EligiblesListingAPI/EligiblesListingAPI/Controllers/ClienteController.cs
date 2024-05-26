using Microsoft.AspNetCore.Mvc;
using EligiblesListingAPI.Application.Interfaces;

namespace EligiblesListingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {

        private readonly IGetEligibleClienteQuery _getEligibleClienteQuery;

        public ClienteController(IGetEligibleClienteQuery getEligibleCustomersQuery)
        {
            _getEligibleClienteQuery = getEligibleCustomersQuery;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string region, [FromQuery] string classification)
        {
            var customers = _getEligibleClienteQuery.Execute(region, classification);
            return Ok(customers);
        }

        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //private readonly ILogger<WeatherForecastController> _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
    }
}
