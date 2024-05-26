using EligiblesListingAPI.Domain.Entities;

namespace EligiblesListingAPI.Domain.Interfaces
{
    public interface IClienteRepository
    {
        IEnumerable<Customer> GetEligibleCliente(string region, string classification);
    }
}
