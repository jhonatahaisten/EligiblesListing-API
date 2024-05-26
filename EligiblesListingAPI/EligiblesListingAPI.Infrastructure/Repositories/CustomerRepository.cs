using EligiblesListingAPI.Domain.Entities;
using EligiblesListingAPI.Domain.Interfaces;
using EligiblesListingAPI.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;


namespace EligiblesListingAPI.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customers = new();

        public IEnumerable<Customer> GetAll()
        {
            return _customers;
        }

        public void Add(Customer customer)
        {
            _customers.Add(customer);
        }
    }
}
