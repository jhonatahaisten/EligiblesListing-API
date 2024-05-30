using EligiblesListingAPI.Domain.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;

namespace EligiblesListingAPI.Test
{
    public class CustomersIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly string eligiblesEndPoint = "/api/customers/eligibles";

        public CustomersIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Post_Eligibles_Returns_FilteredCustomers()
        {
            PagedRequest request = new()
            {
                PageNumber = 8,
                PageSize = 1,
                TotalCount = 10,
                Users =
                [
                    new() { Region = "sul", Type = "laborious" }
                ]
            };

            HttpResponseMessage response = await InvokeCustomersEligiblesAPIAsync(request);

            response.EnsureSuccessStatusCode();

            List<CustomerResponse> expectedCustomers =
            [
                new()
                {
                    Type = "laborious",
                    Gender = "f",
                    Name = new Name { Title = "mrs", First = "melina", Last = "souza" },
                    Location = new Location
                    {
                        Region = "sul",
                        Street = "1856 rua um",
                        City = "garanhuns",
                        State = "santa catarina",
                        Postcode = 51640,
                        Coordinates = new Coordinates { Latitude = "-16.4160", Longitude = "-93.7689" },
                        Timezone = new Timezone { Offset = "+2:00", Description = "Kaliningrad, South Africa" }
                    },
                    Nationality = "BR",
                    Email = "melina.souza@example.com",
                    Birthday = "1963-11-03T13:17:36Z",
                    Registered = "2015-07-08T17:33:35Z",
                    TelephoneNumbers = ["+552894017853"],
                    MobileNumbers = ["+556111414458"],
                    Picture = new Picture
                    {
                        Large = "https://randomuser.me/api/portraits/women/46.jpg",
                        Medium = "https://randomuser.me/api/portraits/med/women/46.jpg",
                        Thumbnail = "https://randomuser.me/api/portraits/thumb/women/46.jpg"
                    }
                }

            ];

            string expectedJson = JsonConvert.SerializeObject(expectedCustomers);
            string actualJson = await CustomersEligiblesResponseSerialization(response);

            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public async Task Post_Eligibles_Returns_FilteredCustomers_Paginated()
        {
            PagedRequest request = new()
            {
                PageNumber = 4,
                PageSize = 1,
                TotalCount = 10,
                Users =
                [
                    new CustomerRequest { Region = "nordeste", Type = "laborious" }
                ]
            };

            HttpResponseMessage response = await InvokeCustomersEligiblesAPIAsync(request);

            response.EnsureSuccessStatusCode();

            List<CustomerResponse> expectedCustomers =
            [
                new()
                {
                    Type = "laborious",
                    Gender = "f",
                    Name = new Name { Title = "mrs", First = "hermelinda", Last = "porto" },
                    Location = new Location
                    {
                        Region = "nordeste",
                        Street = "1000 rua pernambuco ",
                        City = "angra dos reis",
                        State = "alagoas",
                        Postcode = 35371,
                        Coordinates = new Coordinates { Latitude = "63.3179", Longitude = "-64.1995" },
                        Timezone = new Timezone { Offset = "-2:00", Description = "Mid-Atlantic" }
                    },
                    Nationality = "BR",
                    Email = "hermelinda.porto@example.com",
                    Birthday = "1990-11-05T06:14:15Z",
                    Registered = "2010-11-15T06:51:20Z",
                    TelephoneNumbers = ["+554277921297"],
                    MobileNumbers = ["+554471613755"],
                    Picture = new Picture
                    {
                        Large = "https://randomuser.me/api/portraits/women/94.jpg",
                        Medium = "https://randomuser.me/api/portraits/med/women/94.jpg",
                        Thumbnail = "https://randomuser.me/api/portraits/thumb/women/94.jpg"
                    }
                }
            ];

            string expectedJson = JsonConvert.SerializeObject(expectedCustomers);
            string actualJson = await CustomersEligiblesResponseSerialization(response);

            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public async Task Post_Ineligibles_Returns_FilteredCustomers()
        {
            PagedRequest request = new()
            {
                PageNumber = 1,
                PageSize = 1,
                TotalCount = 100,
                Users =
                [
                    new CustomerRequest { Region = "nordeste", Type = "normal" }
                ]
            };

            HttpResponseMessage response = await InvokeCustomersEligiblesAPIAsync(request);

            response.EnsureSuccessStatusCode();

            List<CustomerResponse> expectedCustomers = [];

            string expectedJson = JsonConvert.SerializeObject(expectedCustomers);
            string actualJson = await CustomersEligiblesResponseSerialization(response);

            Assert.Equal(expectedJson, actualJson);
        }

        private async Task<HttpResponseMessage> InvokeCustomersEligiblesAPIAsync(PagedRequest request)
        {
            StringContent content = new(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            return await _client.PostAsync(eligiblesEndPoint, content);
        }

        private static async Task<string> CustomersEligiblesResponseSerialization(HttpResponseMessage response)
        {
            string responseString = await response.Content.ReadAsStringAsync();
            List<CustomerResponse> customers = JsonConvert.DeserializeObject<List<CustomerResponse>>(responseString);

            return JsonConvert.SerializeObject(customers);
        }
    }
}

