using CsvHelper.Configuration;
using EligiblesListingAPI.Domain.Entities;

namespace EligiblesListingAPI.Application.Services
{
    public class RawUserMap : ClassMap<Customer>
    {
        public RawUserMap()
        {
            Map(m => m.Gender).Name("gender");
            Map(m => m.Name.Title).Name("name.title");
            Map(m => m.Name.First).Name("name.first");
            Map(m => m.Name.Last).Name("name.last");
            Map(m => m.Location.Street).Name("location.street");
            Map(m => m.Location.City).Name("location.city");
            Map(m => m.Location.State).Name("location.state");
            Map(m => m.Location.Postcode).Name("location.postcode");
            Map(m => m.Location.Coordinates.Latitude).Name("location.coordinates.latitude");
            Map(m => m.Location.Coordinates.Longitude).Name("location.coordinates.longitude");
            Map(m => m.Location.Timezone.Offset).Name("location.timezone.offset");
            Map(m => m.Location.Timezone.Description).Name("location.timezone.description");
            Map(m => m.Email).Name("email");
            Map(m => m.Birthday.Date).Name("dob.date");
            Map(m => m.Registered.Date).Name("registered.date");
            Map(m => m.Phone).Name("phone");
            Map(m => m.Cell).Name("cell");
            Map(m => m.Picture.Large).Name("picture.large");
            Map(m => m.Picture.Medium).Name("picture.medium");
            Map(m => m.Picture.Thumbnail).Name("picture.thumbnail");
        }
    }
}
