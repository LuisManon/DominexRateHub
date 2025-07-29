using DominexRateOrchestrator.Application.Interfaces;
using DominexRateOrchestrator.Domain.Models;

namespace DominexRateOrchestrator.Application.Services
{
    public class BancoPopularExchangeRateProvider : IExchangeRateProvider
    {
        private readonly HttpClient _httpClient;

        public BancoPopularExchangeRateProvider(HttpClient httpClient)
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
                if (result != null) result.Provider = "Popular";
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
