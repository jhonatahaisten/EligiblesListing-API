using EligiblesListingAPI.Domain.Entities;

namespace EligiblesListingAPI.Domain.Interfaces
{
    public interface ICustomersService
    {
        Task<List<Customer>> GetUsersAsync(int pageNumber, int pageSize);
        Task LoadUsersAsync();
    }
}
