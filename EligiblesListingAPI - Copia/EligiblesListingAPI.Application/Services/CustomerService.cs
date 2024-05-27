using EligiblesListingAPI.Application.Interfaces;
using EligiblesListingAPI.Domain.Entities;
using EligiblesListingAPI.Domain.Enuns;
using PhoneNumbers;
using System.Globalization;


namespace EligiblesListingAPI.Application.Services
{
    public class CustomerService : ICustomerService
    {      

        public string NormalizeGender(string gender)
        {
            return gender.ToLower() == "female" ? "f" : "m";
        }

        public string TransformPhoneNumbers(string phoneNumber, string country)
        {
            return FormatToE164(phoneNumber, country);
        }

        public string AddNationality()
        {
            return "BR";
        }

        public string FormatToE164(string phoneNumber, string country)
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            try
            {
                var number = phoneNumberUtil.Parse(phoneNumber, country);
                return phoneNumberUtil.Format(number, PhoneNumberFormat.E164);
            }
            catch (NumberParseException ex)
            {

                return null;
            }
        }

        public ERegion DetermineRegion(string latitude, string longitude)
        {
            double lat = double.Parse(latitude, CultureInfo.InvariantCulture);
            double lon = double.Parse(longitude, CultureInfo.InvariantCulture);

            if (lat >= -15.0 && lat <= 5.0 && lon >= -75.0 && lon <= -50.0)
                return ERegion.Norte;
            if (lat >= -15.0 && lat <= 0.0 && lon >= -45.0 && lon <= -35.0)
                return ERegion.Nordeste;
            if (lat >= -24.0 && lat <= -15.0 && lon >= -60.0 && lon <= -45.0)
                return ERegion.CentroOeste;
            if (lat >= -24.0 && lat <= -15.0 && lon >= -45.0 && lon <= -35.0)
                return ERegion.Sudeste;
            if (lat <= -24.0 && lon >= -60.0 && lon <= -45.0)
                return ERegion.Sul;

            return ERegion.Desconhecido;
        }
        public EType ClassifyUserByCoordinates(string latitude, string longitude)
        {
            double lat = double.Parse(latitude, CultureInfo.InvariantCulture);
            double lon = double.Parse(longitude, CultureInfo.InvariantCulture);

            if ((lat >= -46.361899 && lat <= -34.276938 && lon >= -2.196998 && lon <= -15.411580) ||
                (lat >= -52.997614 && lat <= -44.428305 && lon >= -19.766959 && lon <= -23.966413))
            {
                return EType.special;
            }

            if (lat >= -54.777426 && lat <= -46.603598 && lon >= -26.155681 && lon <= -34.016466)
            {
                return EType.normal;
            }

            return EType.laborious;
        }

        public CustomerResponse ConvertToUser(Customer rawUser)
        {
            return new CustomerResponse
            {
                Type = ClassifyUserByCoordinates(rawUser.Location.Coordinates.Latitude, rawUser.Location.Coordinates.Longitude),
                Gender = rawUser.Gender.ToLower() == "male" ? "M" : "F",
                Name = new Name
                {
                    Title = rawUser.Name.Title,
                    First = rawUser.Name.First,
                    Last = rawUser.Name.Last
                },
                Location = new Location
                {
                    Region = DetermineRegion(rawUser.Location.Coordinates.Latitude, rawUser.Location.Coordinates.Longitude),
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
                TelephoneNumbers = [TransformPhoneNumbers(rawUser.Phone, rawUser.Nationality)],
                MobileNumbers = [TransformPhoneNumbers(rawUser.Cell, rawUser.Nationality)],
                Picture = rawUser.Picture,
                Nationality = rawUser.Nationality,
               
            };
        }


    }
}
