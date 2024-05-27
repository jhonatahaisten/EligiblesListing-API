

using EligiblesListingAPI.Domain.Entities;

using PhoneNumbers;

namespace EligiblesListingAPI.Application.Services
{
    public class CustomerService
    {
        public string ClassifyCustomer(Customer customer)
        {
            // Implementar a lógica para classificar o cliente com base na região e critérios
            return "laborious"; // Exemplo, substituir com a classificação correta
        }

        public string NormalizeGender(string gender)
        {
            // Implementar a lógica para normalizar o gênero
            return gender.ToLower() == "female" ? "f" : "m"; // Exemplo, substituir com a normalização correta
        }

        public string TransformPhoneNumbers(string phoneNumber, string country)
        {            
            return FormatToE164(phoneNumber, country);
        }

        public void AddNationality(Customer customer)
        {
            customer.Nationality = "BR";
        }

        public void RemoveAgeFields(Customer customer)
        {
            // Implementar a lógica para remover os campos de idade
        }

        public Customer SimplifyStructure(Customer customer)
        {
            // Implementar a lógica para simplificar a estrutura do cliente
            // e usar arrays em campos específicos
            return customer; // Exemplo, substituir com a estrutura simplificada
        }

        public string FormatToE164(string phoneNumber, string country)
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            try
            {
                var number = phoneNumberUtil.Parse(phoneNumber, country);
                return phoneNumberUtil.Format(number, PhoneNumberFormat.E164);
            }
            catch (NumberParseException ex)
            {
                // Lidar com o erro de análise, se necessário
                return null;
            }
        }
    }
}
