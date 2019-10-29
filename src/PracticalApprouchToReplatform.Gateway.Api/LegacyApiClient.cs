using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PracticalApprouchToReplatform.Gateway.Api.Commands;

namespace PracticalApprouchToReplatform.Gateway.Api
{
    public interface ILegacyApiClient
    {
        Task<HttpResponseMessage> CreatePackage(CreatePackageCommand command);
    }
    public class LegacyApiClient: ILegacyApiClient
    {
        private readonly HttpClient _httpClient;

        public LegacyApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("LegacyApiClient");
            _httpClient.BaseAddress = new Uri("http://localhost:4005/api/");
        }
        public async Task<HttpResponseMessage> CreatePackage(CreatePackageCommand command)
        {
            var content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("package", content);
            
            return response;
        }
    }
}