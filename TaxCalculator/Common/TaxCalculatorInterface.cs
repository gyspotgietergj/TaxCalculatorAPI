using Newtonsoft.Json;
using System.Net;
using TaxCalculator.Common.Interfaces;
using TaxCalculatorAPI.Models;

namespace TaxCalculator.Common
{
    public class TaxCalculatorInterface : ITaxCalculatorInterface
    {
        public TaxCalculatorInterface() { } 
        private string ApiUrl
        {
            get
            {
                return "https://localhost:44378/api/PostalTaxTypes/";
            }
        }

        public async Task<PostalTaxTypes> GetPostalTaxDetail(int Id)
        {
            PostalTaxTypes postalTaxType = new PostalTaxTypes();
            var response = await DoARequest(Id.ToString());
            if (response.IsSuccessStatusCode)
            {
                postalTaxType = JsonConvert.DeserializeObject<PostalTaxTypes>(response.Content.ReadAsStringAsync().Result);
            }
            return postalTaxType;
        }

        public async Task<List<PostalTaxTypes>> GetPostalTaxTypes()
        {
            List<PostalTaxTypes> postalTaxTypes = new List<PostalTaxTypes>();
            var response = await DoARequest("");
            if (response.IsSuccessStatusCode)
            {
                postalTaxTypes = JsonConvert.DeserializeObject<List<PostalTaxTypes>>(response.Content.ReadAsStringAsync().Result);
            }
            return postalTaxTypes;
        }

        private async Task<HttpResponseMessage> DoARequest(string endpoint)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (HttpClient client = new HttpClient())
            {
                return await client.GetAsync(ApiUrl + endpoint);
            }
        }
    }
}
