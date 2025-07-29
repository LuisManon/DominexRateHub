using System.Threading.Tasks;
using BanreservasExchangeRateService.Domain.Models;

namespace BanreservasExchangeRateService.Application.Interfaces
{
    public interface IExchangeRateService
    {
        Task<ExchangeResponse> GetRateAsync(ExchangeRequest request);
    }
}
