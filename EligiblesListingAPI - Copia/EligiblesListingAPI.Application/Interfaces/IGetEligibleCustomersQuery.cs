
using EligiblesListingAPI.Domain.Entities;

namespace EligiblesListingAPI.Application.Interfaces
{
    public interface IGetEligibleCustomersQuery
    {
        IEnumerable<Customer> Execute();

    }
}
