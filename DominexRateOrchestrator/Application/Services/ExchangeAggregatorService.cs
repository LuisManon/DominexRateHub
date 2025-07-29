using DominexRateOrchestrator.Application.Interfaces;
using DominexRateOrchestrator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominexRateOrchestrator.Application.Services
{
    public class ExchangeAggregatorService : IExchangeAggregatorService
    {
        private readonly IEnumerable<IExchangeRateProvider> _providers;

        public ExchangeAggregatorService(IEnumerable<IExchangeRateProvider> providers)
        {
            _providers = providers;
        }

        public async Task<ExchangeResponse?> GetBestRateAsync(ExchangeRequest request)
        {
            var tasks = _providers.Select(p => SafeCall(p, request));
            var results = await Task.WhenAll(tasks);

            var validResults = results.Where(r => r != null);
            if (!validResults.Any())
                return null;

            return validResults.OrderByDescending(r => r!.ConvertedAmount).FirstOrDefault();
        }

        private async Task<ExchangeResponse?> SafeCall(IExchangeRateProvider provider, ExchangeRequest request)
        {
            try
            {
                return await provider.GetRateAsync(request);
            }
            catch
            {
                return null;
            }
        }
    }
}
