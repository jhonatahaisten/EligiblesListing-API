using EligiblesListingAPI.Domain.DTO;
using EligiblesListingAPI.Domain.Enums;
using EligiblesListingAPI.Core.Abstractions;
using PhoneNumbers;
using System.Globalization;

namespace EligiblesListingAPI.Core.Resources
{
    public class EnrichCustomerCore : IEnrichCustomerCore
    {
        public List<CustomerResponse> FillCustomers(List<Customer> rawUsers)
        {
            List<CustomerResponse> customerResponses = rawUsers.Select(rawUser => new CustomerResponse
            {
                Nationality = AddNationality(),
                Type = ClassifyUserByCoordinates(rawUser.Location.Coordinates.Latitude, rawUser.Location.Coordinates.Longitude).ToString(),
                Gender = NormalizeGender(rawUser.Gender),
                Name = new Name
                {
                    Title = rawUser.Name.Title,
                    First = rawUser.Name.First,
                    Last = rawUser.Name.Last
                },
                Location = new Location
                {
                    Region = DetermineRegion(rawUser.Location.State).ToString(),
                    Street = rawUser.Location.Street,
                    City = rawUser.Location.City,
                    State = rawUser.Location.State,
                    Postcode = rawUser.Location.Postcode,
                    Coordinates = new Coordinates
                    {
                        Latitude = rawUser.Location.Coordinates.Latitude,
                        Longitude = rawUser.Location.Coordinates.Longitude,
                    },
                    Timezone = new Timezone
                    {
                        Offset = rawUser.Location.Timezone.Offset,
                        Description = rawUser.Location.Timezone.Description
                    }
                },
                Email = rawUser.Email,
                Birthday = rawUser.Birthday.Date.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                Registered = rawUser.Registered.Date.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                TelephoneNumbers = [TransformPhoneNumbers(rawUser.Phone, AddNationality())],
                MobileNumbers = [TransformPhoneNumbers(rawUser.Cell, AddNationality())],
                Picture = rawUser.Picture
            }).ToList();
            return customerResponses;
        }

        private static string NormalizeGender(string gender)
        {
            return gender.Equals("female", StringComparison.CurrentCultureIgnoreCase) ? "f" : "m";
        }

        private static string TransformPhoneNumbers(string phoneNumber, string country)
        {
            return FormatToE164(phoneNumber, country);
        }

        private static string AddNationality()
        {
            return "BR";
        }

        private static string FormatToE164(string phoneNumber, string country)
        {
            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
            PhoneNumber number = phoneNumberUtil.Parse(phoneNumber, country);
            return phoneNumberUtil.Format(number, PhoneNumberFormat.E164);
        }

        private static ERegion DetermineRegion(string state)
        {
            return StateRegionMapping.stateRegionMap.TryGetValue(state.ToUpper(), out ERegion region) ? region : ERegion.desconhecido;
         
        }

        private static EType ClassifyUserByCoordinates(string latitude, string longitude)
        {
            List<(double minLongitude, double minLatitude, double maxLongitude, double maxLatitude)> specialBoxes =
            [
                (-2.196998, -46.361899, -15.411580, -34.276938),
                (-19.766959, -52.997614, -23.966413, -44.428305)
            ];

            List<(double minLongitude, double minLatitude, double maxLongitude, double maxLatitude)> normalBoxes =
            [
                (-26.155681, -54.777426, -34.016466, -46.603598)
            ];

            if (string.IsNullOrEmpty(latitude) || string.IsNullOrEmpty(longitude))
                return EType.laborious;

            double lat = double.Parse(latitude, CultureInfo.InvariantCulture);
            double lon = double.Parse(longitude, CultureInfo.InvariantCulture);

            foreach (var box in specialBoxes)
            {
                if ((lon >= box.minLongitude && lon <= box.maxLongitude) &&
                    (lat >= box.minLatitude && lat <= box.maxLatitude))
                {
                    return EType.special;
                }
            }

            foreach (var box in normalBoxes)
            {
                if ((lon >= box.minLongitude && lon <= box.maxLongitude) &&
                    (lat >= box.minLatitude && lat <= box.maxLatitude))
                {
                    return EType.normal;
                }
            }
            return EType.laborious;
        }
    }
}