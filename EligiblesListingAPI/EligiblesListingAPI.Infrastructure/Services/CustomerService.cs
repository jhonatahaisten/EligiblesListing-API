using EligiblesListingAPI.Domain.Entities;
using EligiblesListingAPI.Domain.Interfaces;

namespace EligiblesListingAPI.Infrastructure.Data
{
    public class DataService : ICustomerService
    {
        private readonly HttpClient _httpClient;

        public DataService(HttpClient httpClient)
        {
            _httpClient = httpClient; //?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<Customer>> GetCustomersFromCsvLink(string csvLink)
        {
            var csvContent = await _httpClient.GetStringAsync(csvLink);
            // Transformar CSV em objetos Customer conforme as regras de negócio
            var customers = new List<Customer>();
            // Implementar a lógica de transformação
            return customers;
        }

        public async Task<IEnumerable<Customer>> GetCustomersFromJsonLink(string jsonLink)
        {
            var jsonContent = await _httpClient.GetStringAsync(jsonLink);
            // Transformar JSON em objetos Customer conforme as regras de negócio
            var customers = new List<Customer>();
            // Implementar a lógica de transformação
            return customers;
        }
    }
}

