using DominexRateOrchestrator.Domain.Models;
using System.Threading.Tasks;

namespace DominexRateOrchestrator.Application.Interfaces
{
    public interface IExchangeAggregatorService
    {
        Task<ExchangeResponse?> GetBestRateAsync(ExchangeRequest request);
    }
}
