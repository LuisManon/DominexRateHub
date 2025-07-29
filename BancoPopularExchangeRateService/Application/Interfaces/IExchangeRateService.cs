using System.Threading.Tasks;
using BancoPopularExchangeRateService.Domain.Models;

namespace BancoPopularExchangeRateService.Application.Interfaces
{
    public interface IExchangeRateService
    {
        Task<ExchangeResponse> GetRateAsync(ExchangeRequest request);
    }
}
