using EligiblesListingAPI.Domain.Entities;

namespace EligiblesListingAPI.Domain.Interfaces
{
    public interface IDataLoadService
    {
        List<CustomerResponse> GetAll();
        void SeedData();
    }
}
