using EligiblesListingAPI.Domain.Entities;

namespace EligiblesListingAPI.Domain.Interfaces
{
    public interface ICustomerService
    {
     
        Task<IEnumerable<Customer>> GetCustomersFromCsvLink(string csvLink);

        Task<IEnumerable<Customer>> GetCustomersFromJsonLink(string jsonLink);
       
    }
}
