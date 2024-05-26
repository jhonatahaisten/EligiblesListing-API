using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using EligiblesListingAPI.Domain.Entities;
using System.Globalization;
using System.Text.Json;
using EligiblesListingAPI.Application.Interfaces;
using EligiblesListingAPI.Domain.Interfaces;
using CsvHelper.Configuration;

namespace EligiblesListingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {

        private readonly IGetEligibleCustomersQuery _getEligibleCustomersQuery;
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(IGetEligibleCustomersQuery getEligibleCustomersQuery, ICustomerRepository customerRepository)
        {
            _getEligibleCustomersQuery = getEligibleCustomersQuery;
            _customerRepository = customerRepository;
        }

        [HttpPost("csv")]
        public IActionResult PostCsv([FromBody] string csvContent)
        {
            var customers = ParseCsv(csvContent);
            foreach (var customer in customers)
            {
                _customerRepository.Add(customer);
            }
            return Ok(customers);
        }

        [HttpPost("json")]
        public IActionResult PostJson([FromBody] JsonElement jsonContent)
        {
            var customer = JsonSerializer.Deserialize<Customer>(jsonContent.GetRawText());
            _customerRepository.Add(customer);
            return Ok(customer);
        }

        private IEnumerable<Customer> ParseCsv(string csvContent)
        {
            using (var reader = new StringReader(csvContent))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                return csv.GetRecords<Customer>().ToList();
            }
        }
    }
}
