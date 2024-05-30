using EligiblesListingAPI.Domain.DTO;
using EligiblesListingAPI.Core.Abstractions;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace EligiblesListingAPI.Infrastructure.Services
{
    public class DataLoadService(IConfiguration configuration) : IDataLoadService
    {
        public List<Customer> customers { get; private set; }
        private readonly IConfiguration _configuration = configuration;

        public void SeedData()
        {
            customers = [];
            LoadCustomersFromCsv(_configuration["SettingsCustomized:UrlDataCsv"]);
            LoadCustomersFromJson(_configuration["SettingsCustomized:UrlDataJson"]);
        }

        public List<Customer> GetAllCustomers()
        {
            return customers;
        }

        private void LoadCustomersFromCsv(string csvUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(csvUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null,
                Delimiter = ",",
                Encoding = Encoding.UTF8
            };

            using StreamReader sr = new(response.GetResponseStream());
            string rawResults = sr.ReadToEnd();
            using StringReader reader = new(rawResults);
            using CsvReader csv = new(reader, config);
            csv.Context.RegisterClassMap<RawUserMap>();
            List<Customer> csvCustomers = csv.GetRecords<Customer>().ToList();
            customers.AddRange(csvCustomers);
        }

        private void LoadCustomersFromJson(string jsonUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(jsonUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using StreamReader streamReader = new(response.GetResponseStream());
            string json = streamReader.ReadToEnd();
            JsonResponse jsonCustomers = JsonConvert.DeserializeObject<JsonResponse>(json);
            customers.AddRange(jsonCustomers.Results);
        }
    }
}