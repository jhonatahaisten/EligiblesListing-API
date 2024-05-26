using EligiblesListingAPI.Application.DTO;
using EligiblesListingAPI.Application.Interfaces;
using EligiblesListingAPI.Domain.Interfaces;

using System.Collections.Generic;
using System.Linq;

namespace EligiblesListingAPI.Application.Services
{
    public class GetEligibleClienteQuery : IGetEligibleClienteQuery
    {
        private readonly IClienteRepository _clienteRepository;

        public GetEligibleClienteQuery(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public IEnumerable<ClienteDto> Execute(string region, string classification)
        {
            var customers = _clienteRepository.GetEligibleCliente(region, classification);
            return customers.Select(c => new ClienteDto { Id = c.Id, Name = c.Name });
        }
    }
}
