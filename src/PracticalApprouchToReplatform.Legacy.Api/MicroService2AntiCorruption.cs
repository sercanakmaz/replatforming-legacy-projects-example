using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace PracticalApprouchToReplatform.Legacy.Api
{
    public interface IMicroService2AntiCorruption
    {
        Task CreatePackage(RemotePackage post);
    }

    public class MicroService2AntiCorruption : IMicroService2AntiCorruption
    {
        private readonly HttpClient _httpClient;

        public MicroService2AntiCorruption(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MicroService2");
            _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
        }

        public async Task CreatePackage(RemotePackage post)
        {
            var content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/posts", content);

            response.EnsureSuccessStatusCode();
        }
    }

    public class RemotePackage
    {
        public RemotePackage(string barcode, string destination)
        {
            Destination = destination;
            UserId = new Random().Next(0, 100000);
        }

        [JsonProperty("Tittle")] 
        public string Barcode { get; set; }
        [JsonProperty("Body")] 
        public string Destination { get; set; }
        public int UserId { get; set; }
    }
}