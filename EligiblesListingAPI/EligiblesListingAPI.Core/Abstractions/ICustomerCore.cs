using EligiblesListingAPI.Domain.DTO;

namespace EligiblesListingAPI.Core.Abstractions
{
    public interface ICustomerCore
    {
        List<CustomerResponse> GetFilteredCustomers(PagedRequest pagedRequest);
    }
}