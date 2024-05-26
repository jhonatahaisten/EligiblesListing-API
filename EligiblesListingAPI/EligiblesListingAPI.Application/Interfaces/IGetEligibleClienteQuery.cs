using EligiblesListingAPI.Application.DTO;

namespace EligiblesListingAPI.Application.Interfaces
{
    public interface IGetEligibleClienteQuery
    {
        IEnumerable<ClienteDto> Execute(string region, string classification);
      
    }
}
