using DominexRateOrchestrator.Domain.Models;

namespace DominexRateOrchestrator.Application.Interfaces
{
    public interface IExchangeRateProvider
    {
        Task<ExchangeResponse?> GetRateAsync(ExchangeRequest request);
    }
}
