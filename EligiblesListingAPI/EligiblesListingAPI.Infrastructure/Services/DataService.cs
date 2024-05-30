using EligiblesListingAPI.Domain.DTO;
using EligiblesListingAPI.Core.Abstractions;
using EligiblesListingAPI.Core.Resources;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text;
using EligiblesListingAPI.Infrastructure.Services;



namespace EligiblesListingAPI.Infrastructure.Data.Services
{
    public class DataService : IDataService
    {
        private readonly ICustomerCore _ICustomerCore;

        public DataService(ICustomerCore ICustomerCore)
        {
            _ICustomerCore = ICustomerCore;
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

            List<Domain.DTO.Customer> rawUsers = new List<Domain.DTO.Customer>(); 

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

                return _ICustomerCore.ConvertToUser(rawUsers);
            }
        }
        public List<CustomerResponse> GetCustomersFromJsonLink(string jsonContent)
        {
            List<Domain.DTO.Customer> rawUsers = JsonConvert.DeserializeObject<List<Domain.DTO.Customer>>(jsonContent);
            return _ICustomerCore.ConvertToUser(rawUsers);
        
        }

    }
}