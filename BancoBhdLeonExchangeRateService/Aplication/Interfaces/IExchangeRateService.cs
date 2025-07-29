using System.Threading.Tasks;
using BancoBhdLeonExchangeRateService.Domain.Models;

namespace BancoBhdLeonExchangeRateService.Application.Interfaces
{
    public interface IExchangeRateService
    {
        Task<ExchangeResponse> GetRateAsync(ExchangeRequest request);
    }
}
