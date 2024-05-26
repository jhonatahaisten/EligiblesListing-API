using EligiblesListingAPI.Domain.Entities;

namespace EligiblesListingAPI.Domain.Interfaces
{
    public interface IClienteRepository
    {
        IEnumerable<Cliente> GetEligibleCliente(string region, string classification);
    }
}
