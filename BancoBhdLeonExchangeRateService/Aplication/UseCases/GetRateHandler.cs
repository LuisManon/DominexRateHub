using System.Threading.Tasks;
using BancoBhdLeonExchangeRateService.Application.Interfaces;
using BancoBhdLeonExchangeRateService.Domain.Models;

namespace BancoBhdLeonExchangeRateService.Application.UseCases
{
    public class GetRateHandler : IExchangeRateService
    {
        public async Task<ExchangeResponse> GetRateAsync(ExchangeRequest request)
        {
            await Task.Delay(150); // Simula latencia
            decimal rate = 60.90m;
            decimal converted = request.Amount * rate;

            return new ExchangeResponse
            {
                Provider = "BHD León",
                Rate = rate,
                ConvertedAmount = converted
            };
        }
    }
}
