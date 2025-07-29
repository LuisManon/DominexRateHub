namespace BanreservasExchangeRateService.Domain.Models
{
    public class ExchangeResponse
    {
        public string Provider { get; set; }
        public decimal Rate { get; set; }
        public decimal ConvertedAmount { get; set; }
    }
}
