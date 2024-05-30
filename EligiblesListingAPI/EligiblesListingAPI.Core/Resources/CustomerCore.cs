using EligiblesListingAPI.Domain.DTO;
using EligiblesListingAPI.Core.Abstractions;

namespace EligiblesListingAPI.Core.Resources
{
    public class CustomerCore(IDataLoadService iDataLoadService, IEnrichCustomerCore iEnrichCustomerCore) : ICustomerCore
    {        
        private readonly List<Customer> _customers = iDataLoadService.GetAllCustomers();
        private readonly IEnrichCustomerCore _iEnrichCustomerCore = iEnrichCustomerCore;

        public List<CustomerResponse> GetFilteredCustomers(PagedRequest pagedRequest)
        {
            List<CustomerResponse> customerResponses = _iEnrichCustomerCore.FillCustomers(_customers);
            List<CustomerResponse> filteredCustomers = [];

            foreach (CustomerRequest user in pagedRequest.Users)
            {
                List<CustomerResponse> filteredCustomer = customerResponses.Where(c => string.Equals(c.Type, user.Type) && string.Equals(c.Location.Region, user.Region)).ToList();
                filteredCustomers.AddRange(filteredCustomer);
            }

            List<CustomerResponse> pagedCustomers = filteredCustomers
                .Take(pagedRequest.TotalCount)
                .Skip((pagedRequest.PageNumber - 1) * pagedRequest.PageSize)
                .Take(pagedRequest.PageSize)
                .ToList();

            return pagedCustomers;
        }
    }
}
