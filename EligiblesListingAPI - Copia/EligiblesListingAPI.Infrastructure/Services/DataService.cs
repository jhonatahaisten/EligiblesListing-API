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

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();

            //return results;
            List<string> splitted = new List<string>();

            string[] tempStr;
            tempStr = results.Split(',');

            //foreach (string item in tempStr)
            //{
            //    if (!string.IsNullOrWhiteSpace(item))
            //    {
            //        splitted.Add(item);
            //    }
            //}

            //var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            //{
            //    HeaderValidated = null
            //};

            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(csvContent);
            //HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            //using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
            //{
            //    string results = sr.ReadToEnd();
            //    List<string> splitted = new List<string>();

            //    string[] tempStr;
            //    tempStr = results.Split(',');

            //    foreach (string item in tempStr)
            //    {
            //        if (!string.IsNullOrWhiteSpace(item))
            //        {
            //            using (var reader = new StringReader(item))
            //            using (var csv = new CsvReader(reader, config))
            //            {
            //                csv.Context.RegisterClassMap<RawUserMap>();
            //                var records = csv.GetRecords<Customer>().ToList();
            //                return records.Select(_iCustomerService.ConvertToUser).ToList();
            //            }
            //          //  splitted.Add(item);
            //        }
            //    }

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null,
                Delimiter = ",",
                Encoding = Encoding.UTF8
            };

            using (var reader = new StringReader(results))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                while (csv.Read())
                {
                    csv.Context.RegisterClassMap<RawUserMap>();
                    var records = csv.GetRecords<CustomerOt>();
                    List<Customer> Xpto = new();
                    
                    csv.Context.RegisterClassMap<RawUserMap>();
                }
                return null;
              //  return records.ToList().Select(_iCustomerService.ConvertToUser).ToList();
            }



        }

        public IEnumerable<CustomerResponse> GetCustomersFromJsonLink(string jsonContent)
        {
            var rawUsers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(jsonContent);
            return rawUsers.Select(_iCustomerService.ConvertToUser).ToList();
        }

    }
}


