using Microsoft.AspNetCore.Mvc;
using EligiblesListingAPI.Domain.DTO;
using EligiblesListingAPI.Core.Abstractions;

namespace EligiblesListingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IDataService _iDataService;
        private readonly ICustomerCore _ICustomerCore;
        private readonly HttpClient _httpClient;

        public CustomersController(IDataService iDataService, ICustomerCore ICustomerCore)
        {
            _iDataService = iDataService;
            _ICustomerCore = ICustomerCore;
            _httpClient = new HttpClient();
        }        

        [HttpPost("Eligibles")]
        public async Task<IActionResult> ClassifyCustomers([FromBody] PagedRequest filterRequest)
        {
            var filteredResponse = _ICustomerCore.GetFilteredCustomers(filterRequest);
            return Ok(filteredResponse);
        }
    }
}
