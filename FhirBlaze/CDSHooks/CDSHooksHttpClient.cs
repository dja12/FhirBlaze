using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FhirBlaze.CDSHooks
{
	public class CDSHooksHttpClient
	{
		private readonly string servicePath = "azurefhir/cds-services";
        private readonly HttpClient http;
        private CDSServices services;

        public CDSHooksHttpClient(HttpClient http)
		{
			this.http = http;
		}

		public string ServiceUrl {
			get {
				return http.BaseAddress.ToString() + servicePath;
			}
		}

		public CDSServices Services
		{
			get { return services; }
		}

		public async Task<CDSServices> DiscoveryAsync()
		{
			//CDSServices services = new CDSServices();

			var resp = await http.GetAsync(servicePath);
			if (resp.IsSuccessStatusCode)
			{
				// Convert to JSON.
				var respString = await resp.Content.ReadAsStringAsync();
				services = JsonConvert.DeserializeObject<CDSServices>(respString);
			}
			else
			{
				System.Console.WriteLine("Error calling Discovery endpoint:" + resp.StatusCode);
			}

            return services;
		}

		public async Task<CDSCards> ServiceAsync(string hookId, CDSRequest hookRequestBody)
		{
			var payload = JsonConvert.SerializeObject(hookRequestBody);
			var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
			var resp = await http.PostAsync(servicePath + "/" + hookId, content);
			CDSCards cards = null;
			if (resp.IsSuccessStatusCode)
			{
				// Convert to JSON.
				var respString = await resp.Content.ReadAsStringAsync();
				cards = JsonConvert.DeserializeObject<CDSCards>(respString);
			}
			else
			{
				System.Console.WriteLine("Error calling Service endpoint:" + resp.StatusCode);
			}

			return cards;

		}
	}
}
