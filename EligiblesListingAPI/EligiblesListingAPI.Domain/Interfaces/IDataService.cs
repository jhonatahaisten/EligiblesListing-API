using EligiblesListingAPI.Domain.Entities;

namespace EligiblesListingAPI.Domain.Interfaces
{
    public interface IDataService
    {      
        IEnumerable<CustomerResponse> GetCustomersFromCsvLink(string csvContent);
        IEnumerable<CustomerResponse> GetCustomersFromJsonLink(string jsonContent);
    }
}
