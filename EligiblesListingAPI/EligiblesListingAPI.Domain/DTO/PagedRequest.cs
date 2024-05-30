namespace EligiblesListingAPI.Domain.DTO
{
    public class PagedRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<CustomerRequest> Users { get; set; }
    }
}
