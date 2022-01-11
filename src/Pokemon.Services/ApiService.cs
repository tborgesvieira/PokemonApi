using Pokemon.Services.Services.Interfaces;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokemon.Services
{
    public class ApiService : IApiService
    {
        private HttpClient _httpClient;
        public ApiService(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["pokeapi"]);

            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _httpClient.GetAsync(url);
        }
    }
}
