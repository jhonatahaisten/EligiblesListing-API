using CsvHelper.Configuration;
using EligiblesListingAPI.Domain.Entities;

namespace EligiblesListingAPI.Application.Services
{
    public class RawUserMap : ClassMap<Customer>
    {
        public RawUserMap()
        {
            Map(m => m.Gender).Name("gender");
            Map(m => m.Name.Title).Name("name__title");
            Map(m => m.Name.First).Name("name__first");
            Map(m => m.Name.Last).Name("name__last");
            Map(m => m.Location.Street).Name("location__street");
            Map(m => m.Location.City).Name("location__city");
            Map(m => m.Location.State).Name("location__state");
            Map(m => m.Location.Postcode).Name("location__postcode");
            Map(m => m.Location.Coordinates.Latitude).Name("location__coordinates__latitude");
            Map(m => m.Location.Coordinates.Longitude).Name("location__coordinates__longitude");
            Map(m => m.Location.Timezone.Offset).Name("location__timezone__offset");
            Map(m => m.Location.Timezone.Description).Name("location__timezone__description");
            Map(m => m.Email).Name("email");
            Map(m => m.Birthday.Date).Name("dob__date");
            Map(m => m.Registered.Date).Name("registered__date");
            Map(m => m.Phone).Name("phone");
            Map(m => m.Cell).Name("cell");
            Map(m => m.Picture.Large).Name("picture__large");
            Map(m => m.Picture.Medium).Name("picture__medium");
            Map(m => m.Picture.Thumbnail).Name("picture__thumbnail");
        }
    }
}
