using EligiblesListingAPI.Application.DTO;
using EligiblesListingAPI.Application.Interfaces;
using EligiblesListingAPI.Domain.Entities;
using EligiblesListingAPI.Domain.Enuns;
using EligiblesListingAPI.Domain.Interfaces;
using PhoneNumbers;
using System.Globalization;

namespace EligiblesListingAPI.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly List<(double minLon, double minLat, double maxLon, double maxLat)> especialBoxes;
        private readonly List<(double minLon, double minLat, double maxLon, double maxLat)> normalBoxes;
        private readonly List<CustomerResponse> _customers;

        public CustomerService(IDataLoadService iDataLoadService)
        {

            _customers = iDataLoadService.GetAll();

            especialBoxes = new List<(double minLon, double minLat, double maxLon, double maxLat)>
            {
                (-2.196998, -46.361899, -15.411580, -34.276938),
                (-19.766959, -52.997614, -23.966413, -44.428305)
            };

            normalBoxes = new List<(double minLon, double minLat, double maxLon, double maxLat)>
            {
                (-26.155681, -54.777426, -34.016466, -46.603598)
            };
        }

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
        public ERegion DetermineRegion(string state)
        {
            return StateRegionMapping.GetRegionByState(state);
        }

        public EType ClassifyUserByCoordinates(string latitude, string longitude)
        {
            if (string.IsNullOrEmpty(latitude) || string.IsNullOrEmpty(longitude))
                return EType.laborious;

            double lat = double.Parse(latitude, CultureInfo.InvariantCulture);
            double lon = double.Parse(longitude, CultureInfo.InvariantCulture);

            foreach (var box in especialBoxes)
            {
                if ((lon >= box.minLon && lon <= box.maxLon) &&
                    (lat >= box.minLat && lat <= box.maxLat))
                {
                    return EType.special;
                }
            }

            foreach (var box in normalBoxes)
            {

                if ((lon >= box.minLon && lon <= box.maxLon) &&
                    (lat >= box.minLat && lat <= box.maxLat))
                {
                    return EType.normal;
                }
            }

            return EType.laborious;
        }

        public List<CustomerResponse> ConvertToUser(List<Customer> rawUsers)
        {
            List<CustomerResponse> customerResponses = rawUsers.Select(rawUser => new CustomerResponse
            {
                Nationality = AddNationality(),
                Type = ClassifyUserByCoordinates(rawUser.Location.Coordinates.Latitude, rawUser.Location.Coordinates.Longitude).ToString(),
                Gender = rawUser.Gender.ToLower() == "male" ? "M" : "F",
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

        public List<CustomerResponse> GetFilteredCustomers(PagedRequest pagedRequest)
        {
             List<CustomerResponse> filteredCustomers = new List<CustomerResponse>();

            foreach (var user in pagedRequest.Users)
            {
                var filteredCustomer = _customers.Where(c =>c.Type == user.Type && c.Location.Region == user.Region);
                filteredCustomers.AddRange(filteredCustomer);     

            }
           // (EType)Enum.Parse(typeof(EType)

            //var customerResponses = ConvertToUser(filteredCustomers);
            List<CustomerResponse> pagedCustomers = filteredCustomers
                .Skip((pagedRequest.PageNumber - 1) * pagedRequest.PageSize)
                .Take(pagedRequest.PageSize)
                .ToList();

            return pagedCustomers;
         
        }

        private List<Customer> GetAllCustomers()
        {

            return new List<Customer>();
        }

    }
}
