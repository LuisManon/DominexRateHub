using DominexRateOrchestrator.Application.Interfaces;
using DominexRateOrchestrator.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DominexRateOrchestrator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExchangeController : ControllerBase
    {
        private readonly IExchangeAggregatorService _aggregatorService;

        public ExchangeController(IExchangeAggregatorService aggregatorService)
        {
            _aggregatorService = aggregatorService;
        }

        [HttpPost]
        public async Task<IActionResult> GetBestRate([FromBody] ExchangeRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.SourceCurrency) ||
                string.IsNullOrWhiteSpace(request.TargetCurrency) ||
                request.Amount <= 0)
            {
                return BadRequest("Parámetros inválidos.");
            }

            var result = await _aggregatorService.GetBestRateAsync(request);

            if (result == null)
                return StatusCode(503, "Ningún proveedor respondió correctamente.");

            return Ok(result);
        }
    }
}
