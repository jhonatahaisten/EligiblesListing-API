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

        public IEnumerable<CustomerResponse> GetCustomersFromCsvLink(string csvContent)
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

            using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
            {
                string results = sr.ReadToEnd();
                List<Customer> rawUsers = new List<Customer>();

                using (var reader = new StringReader(results))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<RawUserMap>();
                    try
                    {
                        csv.Context.RegisterClassMap<RawUserMap>();
                        rawUsers = csv.GetRecords<Customer>().ToList();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error parsing CSV content: " + ex.Message, ex);
                    }
                }

                return records.ToList().Select(_iCustomerService.ConvertToUser).ToList();
            }
        }
        public IEnumerable<CustomerResponse> GetCustomersFromJsonLink(string jsonContent)
        {
            var rawUsers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(jsonContent);
            return rawUsers.Select(_iCustomerService.ConvertToUser).ToList();
        }

    }
}