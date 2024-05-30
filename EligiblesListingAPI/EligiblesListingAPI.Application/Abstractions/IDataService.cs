using EligiblesListingAPI.Domain.DTO;

namespace EligiblesListingAPI.Core.Abstractions
{
    public interface IDataService
    {      
        List<CustomerResponse> GetCustomersFromCsvLink(string csvContent);
        List<CustomerResponse> GetCustomersFromJsonLink(string jsonContent);
    }
}