using EligiblesListingAPI.Domain.Entities;

namespace EligiblesListingAPI.Domain.Interfaces
{
    public interface IDataService
    {      
        List<CustomerResponse> GetCustomersFromCsvLink(string csvContent);
        List<CustomerResponse> GetCustomersFromJsonLink(string jsonContent);
    }
}
