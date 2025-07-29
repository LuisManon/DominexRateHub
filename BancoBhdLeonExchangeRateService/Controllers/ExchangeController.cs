using Microsoft.AspNetCore.Mvc;
using BancoBhdLeonExchangeRateService.Application.Interfaces;
using BancoBhdLeonExchangeRateService.Domain.Models;

namespace BancoBhdLeonExchangeRateService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExchangeController : ControllerBase
    {
        private readonly IExchangeRateService _service;

        public ExchangeController(IExchangeRateService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> PostRate([FromBody] ExchangeRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.SourceCurrency) ||
                string.IsNullOrWhiteSpace(request.TargetCurrency) ||
                request.Amount <= 0)
            {
                return BadRequest("Parámetros inválidos.");
            }

            var result = await _service.GetRateAsync(request);
            return Ok(result);
        }
    }
}
