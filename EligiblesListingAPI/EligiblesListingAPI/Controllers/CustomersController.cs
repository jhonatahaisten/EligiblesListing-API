using Microsoft.AspNetCore.Mvc;
using EligiblesListingAPI.Domain.Entities;
using EligiblesListingAPI.Application.Interfaces;
using EligiblesListingAPI.Domain.Interfaces;
using EligiblesListingAPI.Application.Services;
using EligiblesListingAPI.Application.DTO;

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

        [HttpPost("Eligibles")]
        public async Task<IActionResult> ClassifyCustomers([FromBody] PagedRequest filterRequest)
        {
            var filteredResponse = _iCustomerService.GetFilteredCustomers(filterRequest);
            return Ok(filteredResponse);
        }
    }
}
