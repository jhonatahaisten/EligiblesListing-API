using EligiblesListingAPI.Application.DTO;

namespace EligiblesListingAPI.Application.Interfaces
{
    public interface IGetEligibleCustomersQuery
    {
        IEnumerable<ClienteDto> Execute(string region, string classification);
      
    }
}
