using EligiblesListingAPI.Domain.DTO;
using EligiblesListingAPI.Domain.Enuns;

namespace EligiblesListingAPI.Core.Abstractions
{
    public interface ICustomerCore
    {     
        string NormalizeGender(string gender);
        string TransformPhoneNumbers(string phoneNumber, string country);
        string AddNationality();
        string FormatToE164(string phoneNumber, string country);
        ERegion DetermineRegion(string state);       
        EType ClassifyUserByCoordinates(string latitude, string longitude);        
        List<CustomerResponse> GetFilteredCustomers(PagedRequest filter);
        List<CustomerResponse> ConvertToUser(List<Customer> rawUsers);

    }
}