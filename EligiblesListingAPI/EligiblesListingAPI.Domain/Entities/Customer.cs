namespace EligiblesListingAPI.Domain.Entities
{
    public class Customer
    {
        public string Id { get; set; }
        public string Gender { get; set; }
        public Name Name { get; set; }
        public Location Location { get; set; }
        public string Nationality { get; set; }
        public string Email { get; set; }
        public DateOfBirth DateOfBirth { get; set; }
        public Registered Registered { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public Picture Picture { get; set; }        
        
    }
}