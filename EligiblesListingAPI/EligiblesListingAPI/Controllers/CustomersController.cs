using CsvHelper;using Microsoft.AspNetCore.Mvc;

using EligiblesListingAPI.Domain.Entities;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using EligiblesListingAPI.Application.Interfaces;

namespace EligiblesListingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {

        private readonly IGetEligibleCustomersQuery _getEligibleCustomersQuery;

        public CustomersController(IGetEligibleCustomersQuery getEligibleCustomersQuery)
        {
            _getEligibleCustomersQuery = getEligibleCustomersQuery;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string region, [FromQuery] string classification)
        {
            var customers = _getEligibleCustomersQuery.Execute(region, classification);
            return Ok(customers);
        }

        [HttpPost("csv")]
        public IActionResult PostCsv([FromBody] string csvContent)
        {
            var customers = ParseCsv(csvContent);
            // Process customers as needed
            return Ok(customers);
        }

        [HttpPost("json")]
        public IActionResult PostJson([FromBody] JsonElement jsonContent)
        {
            var customer = JsonSerializer.Deserialize<Customer>(jsonContent.GetRawText());
            // Process customer as needed
            return Ok(customer);
        }

        private IEnumerable<Customer> ParseCsv(string csvContent)
        {
            using (var reader = new StringReader(csvContent))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<Customer>().ToList();
            }
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
