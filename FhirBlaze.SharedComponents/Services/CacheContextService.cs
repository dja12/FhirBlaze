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

        public async Task SaveConext(string token, string userId, string encodedCtx)
		{
            var cacheInfo = new
            {
                userId = userId,
                launch = encodedCtx
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
