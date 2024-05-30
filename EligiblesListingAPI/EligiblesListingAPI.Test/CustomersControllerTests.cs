using EligiblesListingAPI.Domain.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;

namespace EligiblesListingAPI.Test
{
    public class CustomerFixture : IClassFixture<WebApplicationFactory<Program>>
    {
        
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly string eligiblesEndPoint = "/api/customers/eligibles";

        public CustomerFixture(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }
        private async Task<HttpResponseMessage> InvokeCustomersEligiblesAPIAsync(PagedRequest request)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            return  await _client.PostAsync(eligiblesEndPoint, content);
        }
        private async Task<string> CustomersEligiblesResponseSerialization(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<List<CustomerResponse>>(responseString);

            return JsonConvert.SerializeObject(customers);
        }

        [Fact]
        public async Task Post_Eligibles_Returns_FilteredCustomers()
        {
            PagedRequest request = new PagedRequest
            {
                PageNumber = 1,
                PageSize = 1,
                TotalCount = 2,
                Users = new List<CustomerRequest>
                {
                    new CustomerRequest { Region = "sul", Type = "laborious" }
                }
            };

            HttpResponseMessage response = await InvokeCustomersEligiblesAPIAsync(request);
        
            response.EnsureSuccessStatusCode();

            List<CustomerResponse> expectedCustomers = new List<CustomerResponse>
            {
                new CustomerResponse
                {
                    Type = "laborious",
                    Gender = "F",
                    Name = new Name { Title = "mrs", First = "iza", Last = "gonçalves" },
                    Location = new Location
                    {
                        Region = "sul",
                        Street = "3558 rua pernambuco ",
                        City = "juazeiro",
                        State = "santa catarina",
                        Postcode = 53106,
                        Coordinates = new Coordinates { Latitude = "-2.6880", Longitude = "-146.8402" },
                        Timezone = new Timezone { Offset = "-4:00", Description = "Atlantic Time (Canada), Caracas, La Paz" }
                    },
                    Nationality = "BR",
                    Email = "iza.gonçalves@example.com",
                    Birthday = "1948-08-07T06:02:24Z",
                    Registered = "2009-01-20T08:25:28Z",
                    TelephoneNumbers = new[] { "+558130299534" },
                    MobileNumbers = new[] { "+558715674325" },
                    Picture = new Picture
                    {
                        Large = "https://randomuser.me/api/portraits/women/85.jpg",
                        Medium = "https://randomuser.me/api/portraits/med/women/85.jpg",
                        Thumbnail = "https://randomuser.me/api/portraits/thumb/women/85.jpg"
                    }
                },

            };

            string expectedJson = JsonConvert.SerializeObject(expectedCustomers);
            string actualJson = await CustomersEligiblesResponseSerialization(response);

            Assert.Equal(expectedJson, actualJson);

        }

        [Fact]
        public async Task Post_Eligibles_Returns_FilteredCustomers_Paginated()
        {
            var request = new PagedRequest
            {
                PageNumber = 10,
                PageSize = 1,
                TotalCount = 15,
                Users = new List<CustomerRequest>
                {
                    new CustomerRequest { Region = "nordeste", Type = "laborious" }
                }
            };

            HttpResponseMessage response = await InvokeCustomersEligiblesAPIAsync(request);

            response.EnsureSuccessStatusCode();

            List<CustomerResponse> expectedCustomers = new List<CustomerResponse>
            {
                new CustomerResponse
                {
                    Type = "laborious",
                    Gender = "F",
                    Name = new Name { Title = "ms", First = "jerueza", Last = "moura" },
                    Location = new Location
                    {
                        Region = "nordeste",
                        Street = "416 rua três",
                        City = "magé",
                        State = "alagoas",
                        Postcode = 73804,
                        Coordinates = new Coordinates { Latitude = "15.3004", Longitude = "-82.4361" },
                        Timezone = new Timezone { Offset = "+4:00", Description = "Abu Dhabi, Muscat, Baku, Tbilisi" }
                    },
                    Nationality = "BR",
                    Email = "jerueza.moura@example.com",
                    Birthday = "1948-08-29T06:01:44Z",
                    Registered = "2013-11-28T12:04:46Z",
                    TelephoneNumbers = new[] { "+555667280423" },
                    MobileNumbers = new[] { "+551840117309" },
                    Picture = new Picture
                    {
                        Large = "https://randomuser.me/api/portraits/women/14.jpg",
                        Medium = "https://randomuser.me/api/portraits/med/women/14.jpg",
                        Thumbnail = "https://randomuser.me/api/portraits/thumb/women/14.jpg"
                    }
                }
            };

            string expectedJson = JsonConvert.SerializeObject(expectedCustomers);
            string actualJson = await CustomersEligiblesResponseSerialization(response);


            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public async Task Post_Ineligibles_Returns_FilteredCustomers()
        {
            PagedRequest request = new PagedRequest
            {
                PageNumber = 1,
                PageSize = 1,
                TotalCount = 100,
                Users = new List<CustomerRequest>
                {
                    new CustomerRequest { Region = "nordeste", Type = "normal" }
                }
            };

            HttpResponseMessage response = await InvokeCustomersEligiblesAPIAsync(request);

            response.EnsureSuccessStatusCode();

            List<CustomerResponse> expectedCustomers = new List<CustomerResponse>();

            string expectedJson = JsonConvert.SerializeObject(expectedCustomers);
            string actualJson = await CustomersEligiblesResponseSerialization(response);


            Assert.Equal(expectedJson, actualJson);
        }
    }
}