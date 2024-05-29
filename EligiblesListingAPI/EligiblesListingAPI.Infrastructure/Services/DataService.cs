using EligiblesListingAPI.Domain.Entities;
using EligiblesListingAPI.Application.Interfaces;
using EligiblesListingAPI.Domain.Interfaces;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using EligiblesListingAPI.Application.Services;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text;


namespace EligiblesListingAPI.Infrastructure.Data
{
    public class DataService : IDataService
    {
        private readonly ICustomerService _iCustomerService;

        public DataService(ICustomerService iCustomerService)
        {
            _iCustomerService = iCustomerService;
        }

        public List<CustomerResponse> GetCustomersFromCsvLink(string csvContent)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(csvContent);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null,
                Delimiter = ",",
                Encoding = Encoding.UTF8
            };

            List<Customer> rawUsers = new List<Customer>(); 

            using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
            {
                string results = sr.ReadToEnd();

                using (var reader = new StringReader(results))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<RawUserMap>();
                    csv.Context.RegisterClassMap<RawUserMap>();
                    rawUsers = csv.GetRecords<Customer>().ToList();

                }

                return _iCustomerService.ConvertToUser(rawUsers);
            }
        }
        public List<CustomerResponse> GetCustomersFromJsonLink(string jsonContent)
        {
            List<Customer> rawUsers = JsonConvert.DeserializeObject<List<Customer>>(jsonContent);
            return _iCustomerService.ConvertToUser(rawUsers);
        
        }

    }
}