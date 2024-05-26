using EligiblesListingAPI.Application.DTO;
using EligiblesListingAPI.Application.Interfaces;
using EligiblesListingAPI.Domain.Entities;
using EligiblesListingAPI.Domain.Interfaces;

using System.Collections.Generic;
using System.Linq;

namespace EligiblesListingAPI.Application.Services
{
    public class GetEligibleCustomersQuery : IGetEligibleCustomersQuery
    {
        private readonly ICustomerRepository _customerRepository;

        public GetEligibleCustomersQuery(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<Customer> Execute()
        {
            return _customerRepository.GetAll();
        }
    }
}
