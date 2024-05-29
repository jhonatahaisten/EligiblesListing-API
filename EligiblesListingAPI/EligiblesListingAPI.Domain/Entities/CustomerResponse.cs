using EligiblesListingAPI.Domain.Enuns;

namespace EligiblesListingAPI.Domain.Entities
{  
    public class CustomerResponse
    {
       // public string Id { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public Name Name { get; set; }
        public Location Location { get; set; }
        public string Nationality { get; set; }
        public string Email { get; set; }
        public string Birthday { get; set; }
        public string Registered { get; set; }
        public string[] TelephoneNumbers { get; set; }
        public string[] MobileNumbers { get; set; }
        public Picture Picture { get; set; }
    }
}
