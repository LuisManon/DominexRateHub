using System.Threading.Tasks;
using BanreservasExchangeRateService.Application.Interfaces;
using BanreservasExchangeRateService.Domain.Models;

namespace BanreservasExchangeRateService.Application.UseCases
{
    public class GetRateHandler : IExchangeRateService
    {
        public async Task<ExchangeResponse> GetRateAsync(ExchangeRequest request)
        {
            await Task.Delay(100); // Simula latencia o llamada externa
            decimal rate = 57.25m;
            decimal converted = request.Amount * rate;

            return new ExchangeResponse
            {
                Provider = "Banreservas",
                Rate = rate,
                ConvertedAmount = converted
            };
        }
    }
}
