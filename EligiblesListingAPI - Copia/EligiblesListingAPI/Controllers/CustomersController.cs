using Microsoft.AspNetCore.Mvc;
using EligiblesListingAPI.Domain.Entities;
using EligiblesListingAPI.Application.Interfaces;
using EligiblesListingAPI.Domain.Interfaces;

namespace EligiblesListingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IDataService _iDataService;
        private readonly ICustomerService _iCustomerService;
        private readonly HttpClient _httpClient;

        public CustomersController(IDataService iDataService, ICustomerService iCustomerService)
        {
            _iDataService = iDataService;
            _iCustomerService = iCustomerService;
            _httpClient = new HttpClient();
        }
          


        [HttpGet("classify")]
        public async Task<IActionResult> ClassifyCustomers([FromQuery] string dataLink)
        {
            if (!(await _httpClient.GetAsync(dataLink)).IsSuccessStatusCode)
            {
                return BadRequest("Failed to download file.");
            }         

            IEnumerable<CustomerResponse> dataCustomers;      

            if (dataLink.EndsWith(".csv"))
                dataCustomers =  _iDataService.GetCustomersFromCsvLink(dataLink);
            else if (dataLink.EndsWith(".json"))
                dataCustomers =  _iDataService.GetCustomersFromJsonLink(dataLink);
            else
                return BadRequest("Invalid file type.");
       

            return Ok(dataCustomers);
        }
    }
}
