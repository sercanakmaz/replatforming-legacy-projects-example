using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PracticalApprouchToReplatform.Gateway.Api.Commands;

namespace PracticalApprouchToReplatform.Gateway.Api
{
    public interface INewApiClient
    {
        Task<HttpResponseMessage> CreateDelivery(CreateDeliveryCommand command);
    }

    public class NewApiClient : INewApiClient
    {
        private readonly HttpClient _httpClient;

        public NewApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("NewApiClient");
            _httpClient.BaseAddress = new Uri("http://localhost:5005/api/");
        }
        public async Task<HttpResponseMessage> CreateDelivery(CreateDeliveryCommand command)
        {
            var content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("delivery", content);
            
            return response;
        }
    }
}