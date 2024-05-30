using Newtonsoft.Json;

namespace EligiblesListingAPI.Domain.DTO
{
    public class JsonResponse
    {
        [JsonProperty("results")]
        public List<Customer> Results { get; set; }
    }

}
