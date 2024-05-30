using EligiblesListingAPI.Domain.DTO;
using EligiblesListingAPI.Core.Abstractions;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace EligiblesListingAPI.Infrastructure.Services
{
    public class DataLoadService : IDataLoadService
    {
        public List<Customer> Customers { get; private set; }     
    
        public void SeedData()
        {
            Customers = new List<Customer>();
            LoadCustomersFromCsv("https://storage.googleapis.com/juntossomosmais-code-challenge/input-backend.csv");
            LoadCustomersFromJson("https://storage.googleapis.com/juntossomosmais-code-challenge/input-backend.json");
        }     
        
        public List<Customer> GetAll()
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
                    Customers.AddRange(csvCustomers);
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
                Customers.AddRange(jsonCustomers.Results);
            }
        }        

        public class JsonResponse
        {
            [JsonProperty("results")]
            public List<Customer> Results { get; set; }
        }
    }
}