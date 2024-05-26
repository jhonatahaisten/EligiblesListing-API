using EligiblesListingAPI.Domain.Entities;
using EligiblesListingAPI.Domain.Interfaces;
using EligiblesListingAPI.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;


namespace EligiblesListingAPI.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClienteContext _context;

        public ClienteRepository(ClienteContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetEligibleCliente(string region, string classification)
        {
            return _context.Customers
                           .Where(c => c.Region == region && c.Classification == classification)
                           .ToList();
        }
    }
}
