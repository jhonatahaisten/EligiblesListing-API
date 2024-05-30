using Microsoft.AspNetCore.Mvc;
using EligiblesListingAPI.Domain.DTO;
using EligiblesListingAPI.Core.Abstractions;

namespace EligiblesListingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController(ICustomerCore ICustomerCore) : ControllerBase
    {
        private readonly ICustomerCore _ICustomerCore = ICustomerCore;

        [HttpPost("Eligibles")]
        public async Task<IActionResult> ClassifyCustomers([FromBody] PagedRequest filterRequest)
        {
            List<CustomerResponse> filteredResponse = _ICustomerCore.GetFilteredCustomers(filterRequest);
            return Ok(filteredResponse);
        }
    }
}
