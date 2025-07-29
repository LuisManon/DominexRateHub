using System.Threading.Tasks;
using BancoPopularExchangeRateService.Application.Interfaces;
using BancoPopularExchangeRateService.Domain.Models;

namespace BancoPopularExchangeRateService.Application.UseCases
{
    public class GetRateHandler : IExchangeRateService
    {
        public async Task<ExchangeResponse> GetRateAsync(ExchangeRequest request)
        {
            await Task.Delay(120); // Simula latencia
            decimal rate = 58.10m;
            decimal converted = request.Amount * rate;

            return new ExchangeResponse
            {
                Provider = "Popular",
                Rate = rate,
                ConvertedAmount = converted
            };
        }
    }
}
