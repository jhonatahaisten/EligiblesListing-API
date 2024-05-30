using EligiblesListingAPI.Domain.DTO;
using EligiblesListingAPI.Core.Abstractions;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using EligiblesListingAPI.Infrastructure.Services;

namespace EligiblesListingAPI.Infrastructure.Data.Services
{
    public class FileService(IEnrichCustomerCore iEnrichCustomerCore) : IFileService
    {       
        private readonly IEnrichCustomerCore _iEnrichCustomerCore = iEnrichCustomerCore;

        public List<CustomerResponse> GetCustomersFromCsvLink(string csvContent)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(csvContent);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null,
                Delimiter = ",",
                Encoding = Encoding.UTF8
            };

            List<Customer> rawUsers = [];

            using StreamReader sr = new(response.GetResponseStream());
            string results = sr.ReadToEnd();

            using (StringReader reader = new(results))
            using (CsvReader csv = new(reader, config))
            {
                csv.Context.RegisterClassMap<RawUserMap>();
                csv.Context.RegisterClassMap<RawUserMap>();
                rawUsers = csv.GetRecords<Customer>().ToList();
            }
            return _iEnrichCustomerCore.FillCustomers(rawUsers);
        }

        public List<CustomerResponse> GetCustomersFromJsonLink(string jsonContent)
        {
            List<Customer> rawUsers = JsonConvert.DeserializeObject<List<Customer>>(jsonContent);
            return _iEnrichCustomerCore.FillCustomers(rawUsers);        
        }
    }
}