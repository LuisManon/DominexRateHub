namespace BancoPopularExchangeRateService.Domain.Models
{
    public class ExchangeRequest
    {
        public string SourceCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public decimal Amount { get; set; }
    }
}
