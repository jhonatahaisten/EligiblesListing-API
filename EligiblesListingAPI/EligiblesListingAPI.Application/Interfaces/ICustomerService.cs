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
        ERegion DetermineRegion(string latitude, string longitude);
        CustomerResponse ConvertToUser(Customer rawUser);
        EType ClassifyUserByCoordinates(string latitude, string longitude);
    }
}
