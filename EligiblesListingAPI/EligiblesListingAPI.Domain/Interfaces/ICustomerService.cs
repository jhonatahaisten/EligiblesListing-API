using EligiblesListingAPI.Domain.Entities;

namespace EligiblesListingAPI.Domain.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomersAsync();
        Task LoadCustomersAsync();

    }
}
