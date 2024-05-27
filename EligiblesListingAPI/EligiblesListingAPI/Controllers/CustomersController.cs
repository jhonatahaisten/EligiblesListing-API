using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using EligiblesListingAPI.Domain.Entities;
using System.Globalization;
using System.Text.Json;
using EligiblesListingAPI.Application.Interfaces;
using EligiblesListingAPI.Application.Services;
using EligiblesListingAPI.Domain.Interfaces;
using CsvHelper.Configuration;
using EligiblesListingAPI.Infrastructure.Data;
using System.Diagnostics.Metrics;

namespace EligiblesListingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly DataService _dataService;
        private readonly CustomerService _customerService;

        public CustomersController(DataService dataService, CustomerService customerService)
        {
            _dataService = dataService;
            _customerService = customerService;
        }

        [HttpGet("classify")]
        public async Task<IActionResult> ClassifyCustomers([FromQuery] string dataLink)
        {
            IEnumerable<Customer> dataCustomers;
      

            if (dataLink.EndsWith(".csv"))
                dataCustomers = await _dataService.GetCustomersFromCsvLink(dataLink);
            else
                dataCustomers = await _dataService.GetCustomersFromJsonLink(dataLink);

            string country = "BR";

            // Combinar clientes de ambos os links
           // var allCustomers = new List<Customer>();
         //   allCustomers.AddRange(dataCustomers);
        

            // Aplicar transformações e classificar os clientes
            var classifiedCustomers = new List<object>();
            foreach (var customer in dataCustomers)
            {
                var normalizedGender = _customerService.NormalizeGender(customer.Gender);
                var classifiedType = _customerService.ClassifyCustomer(customer);

                customer.Gender = normalizedGender;
                _customerService.TransformPhoneNumbers(customer.Phone, country);
                _customerService.AddNationality(customer);
                _customerService.RemoveAgeFields(customer);

                var simplifiedCustomer = _customerService.SimplifyStructure(customer);

                classifiedCustomers.Add(new
                {
                    type = classifiedType,
                    customer = simplifiedCustomer
                });
            }

            return Ok(classifiedCustomers);
        }
    }
}
