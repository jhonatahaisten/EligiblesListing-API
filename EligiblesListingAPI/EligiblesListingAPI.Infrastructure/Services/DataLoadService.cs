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
using System.Net.Http.Json;


namespace EligiblesListingAPI.Infrastructure.Data
{
    public class DataLoadService : IDataLoadService
    {
        public List<CustomerResponse> Customers { get; private set; }
        private readonly ICustomerService _iCustomerService;

        public void SeedData()
        {
            Customers = new List<CustomerResponse>();
            LoadCustomersFromCsv("https://storage.googleapis.com/juntossomosmais-code-challenge/input-backend.csv");
            LoadCustomersFromJson("https://storage.googleapis.com/juntossomosmais-code-challenge/input-backend.json");
        }

        public DataLoadService (ICustomerService iCustomerService)
        {
            _iCustomerService = iCustomerService;
        }

        public List<CustomerResponse> GetAll()
        {
            return Customers;
        }

        private void LoadCustomersFromCsv(string csvUrl)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(csvUrl);
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
                using (var reader = new StringReader(results))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<RawUserMap>();
                    var csvCustomers = csv.GetRecords<Customer>().ToList();
                    var customerResponses = _iCustomerService.ConvertToUser(csvCustomers);
                    Customers.AddRange(customerResponses);
                }
            }
        }

        private void LoadCustomersFromJson(string jsonUrl)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(jsonUrl);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
            {               
                string json = sr.ReadToEnd();

                var jsonCustomers = JsonConvert.DeserializeObject<JsonResponse>(json);
                var customerResponses = _iCustomerService.ConvertToUser(jsonCustomers.Results);
                Customers.AddRange(customerResponses);
            }
        }
        public class JsonResponse
        {
            [JsonProperty("results")]
            public List<Customer> Results { get; set; }
        }
    }
}