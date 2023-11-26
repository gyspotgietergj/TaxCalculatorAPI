using Newtonsoft.Json;
using System.Net;
using System.Text;
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
                return "http://localhost:10000/api/PostalTaxTypes/";
            }
        }

        public async Task<bool> CreatePostalTaxType(PostalTaxTypes postalTaxTypes)
        {
            return await DoAPost(postalTaxTypes);
        }

        public async Task<PostalTaxTypes> GetPostalTaxDetail(int Id)
        {
            PostalTaxTypes postalTaxType = new PostalTaxTypes();
            var response = await DoAGet(Id.ToString());
            if (response.IsSuccessStatusCode)
            {
                postalTaxType = JsonConvert.DeserializeObject<PostalTaxTypes>(response.Content.ReadAsStringAsync().Result);
            }
            return postalTaxType;
        }

        public async Task<List<PostalTaxTypes>> GetPostalTaxTypes()
        {
            List<PostalTaxTypes> postalTaxTypes = new List<PostalTaxTypes>();
            var response = await DoAGet("");
            if (response.IsSuccessStatusCode)
            {
                postalTaxTypes = JsonConvert.DeserializeObject<List<PostalTaxTypes>>(response.Content.ReadAsStringAsync().Result);
            }
            return postalTaxTypes;
        }

        public async Task<bool> UpdatePostalTaxType(int id, PostalTaxTypes postalTaxTypes)
        {
            return await DoAPut(id, postalTaxTypes);
        }

        public async Task<bool> DeletePostalTaxType(int id)
        {
            return await DoADelete(id.ToString());
        }

        private async Task<HttpResponseMessage> DoAGet(string endpoint)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (HttpClient client = new HttpClient())
            {
                return await client.GetAsync(ApiUrl + endpoint);
            }
        }
        private async Task<bool> DoAPut(int Id, PostalTaxTypes postalTaxTypes, string endpoint = "")
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (HttpClient client = new HttpClient())
            {
                var result = await client.PutAsJsonAsync(ApiUrl + endpoint + Id, postalTaxTypes);
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
        private async Task<bool> DoAPost(PostalTaxTypes postalTaxTypes, string endpoint = "")
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (HttpClient client = new HttpClient())
            {
                var result = await client.PostAsJsonAsync(ApiUrl + endpoint, postalTaxTypes);
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
        private async Task<bool> DoADelete(string Id)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (HttpClient client = new HttpClient())
            {
                var result = await client.DeleteAsync(ApiUrl + Id);
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

    }
}
