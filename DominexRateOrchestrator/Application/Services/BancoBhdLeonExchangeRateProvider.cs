using DominexRateOrchestrator.Application.Interfaces;
using DominexRateOrchestrator.Domain.Models;
using System.Net.Http.Json;

namespace DominexRateOrchestrator.Application.Services
{
    public class BancoBhdLeonExchangeRateProvider : IExchangeRateProvider
    {
        private readonly HttpClient _httpClient;

        public BancoBhdLeonExchangeRateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ExchangeResponse?> GetRateAsync(ExchangeRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/exchange", request);

                if (!response.IsSuccessStatusCode) return null;

                var result = await response.Content.ReadFromJsonAsync<ExchangeResponse>();
                if (result != null) result.Provider = "BHD León";
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
