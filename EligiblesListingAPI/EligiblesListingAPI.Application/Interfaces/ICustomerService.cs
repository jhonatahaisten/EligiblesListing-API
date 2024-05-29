using EligiblesListingAPI.Application.DTO;
using EligiblesListingAPI.Domain.Entities;
using EligiblesListingAPI.Domain.Enuns;

namespace EligiblesListingAPI.Application.Interfaces
{
    public interface ICustomerService
    {     
        string NormalizeGender(string gender);
        string TransformPhoneNumbers(string phoneNumber, string country);
        string AddNationality();
        string FormatToE164(string phoneNumber, string country);
        ERegion DetermineRegion(string state);
        List<CustomerResponse> ConvertToUser(List<Customer> rawUser);
        EType ClassifyUserByCoordinates(string latitude, string longitude);
        
        List<CustomerResponse> GetFilteredCustomers(PagedRequest filter);

    }
}