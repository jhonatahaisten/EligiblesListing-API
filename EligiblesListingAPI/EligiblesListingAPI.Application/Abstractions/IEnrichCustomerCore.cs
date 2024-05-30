using EligiblesListingAPI.Domain.DTO;

namespace EligiblesListingAPI.Core.Abstractions
{
    public interface IEnrichCustomerCore
    {
        List<CustomerResponse> FillCustomers(List<Customer> rawUsers);
    }
}
