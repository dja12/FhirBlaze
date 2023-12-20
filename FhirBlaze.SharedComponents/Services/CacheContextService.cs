using System;
using Microsoft.Graph;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;
using System.Text;

namespace FhirBlaze.SharedComponents.Services
{
	public class CacheContextService
	{
        private readonly string servicePath = "auth/context-cache";
        private readonly HttpClient http;

        public CacheContextService(HttpClient http)
        {
            this.http = http;
        }

        public string EncodePayload(string payload)
        {
            var bytes = Encoding.UTF8.GetBytes(payload);
            var base64 = Convert.ToBase64String(bytes);
            //DJA-not doing the URL encode because the context cache function app is not expecting
            // the data to be URL encoded.
            //var urlEncode = HttpUtility.UrlEncode(base64);
            return base64;
        }

        public async Task SaveConext(string token, string userId, string patientId, bool navigator = false)
		{
            var launch = new
            {
                patient = patientId,
                viewType = navigator ? "navigator" : "browser"
            };

            var cacheInfo = new
            {
                userId = userId,
                launch = EncodePayload(JsonConvert.SerializeObject(launch))
            };

			var payload = JsonConvert.SerializeObject(cacheInfo);
            var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
            http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var resp = await http.PostAsync(servicePath, content);
            if (!resp.IsSuccessStatusCode)
            {
                System.Console.WriteLine("Error calling context cache endpoint:" + resp.StatusCode);
            }
        }
    }
}
