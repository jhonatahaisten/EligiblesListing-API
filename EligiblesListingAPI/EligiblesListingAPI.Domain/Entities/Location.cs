using EligiblesListingAPI.Domain.Enuns;

namespace EligiblesListingAPI.Domain.Entities
{
    public class Location
    {
        public ERegion Region { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Postcode { get; set; }
        public Coordinates Coordinates { get; set; }
        public Timezone Timezone { get; set; }
    }
}
