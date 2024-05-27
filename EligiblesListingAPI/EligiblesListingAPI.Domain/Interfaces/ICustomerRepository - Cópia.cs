using EligiblesListingAPI.Domain.Entities;

namespace EligiblesListingAPI.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        void Add(Customer customer);
    }
}
