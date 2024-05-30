using EligiblesListingAPI.Domain.DTO;

namespace EligiblesListingAPI.Core.Abstractions
{
    public interface IDataLoadService
    {
        List<Customer> GetAllCustomers();
        void SeedData();
    }
}