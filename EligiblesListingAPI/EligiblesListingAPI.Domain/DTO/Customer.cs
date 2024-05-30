using EligiblesListingAPI.Domain.Enums;
using Newtonsoft.Json;

namespace EligiblesListingAPI.Domain.DTO
{
    public class Customer
    {
        public EType? Type { get; set; }
        public string Gender { get; set; }
        public Name Name { get; set; }
        public Location Location { get; set; }
        public string? Nationality { get; set; }
        public string Email { get; set; }
        [JsonProperty("dob")]
        public DateOfBirth Birthday { get; set; }
        public Registered Registered { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public Picture Picture { get; set; }

    }
}