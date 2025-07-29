using Microsoft.AspNetCore.Mvc;
using BanreservasExchangeRateService.Application.Interfaces;
using BanreservasExchangeRateService.Domain.Models;

namespace BanreservasExchangeRateService.Controllers
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
        public async Task<IActionResult> GetRate([FromBody] ExchangeRequest request)
        {
            if (request.Amount <= 0 || string.IsNullOrWhiteSpace(request.SourceCurrency) || string.IsNullOrWhiteSpace(request.TargetCurrency))
                return BadRequest("Parámetros inválidos");

            var result = await _service.GetRateAsync(request);
            return Ok(result);
        }
    }
}
