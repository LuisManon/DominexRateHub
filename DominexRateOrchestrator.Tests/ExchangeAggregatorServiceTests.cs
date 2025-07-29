using DominexRateOrchestrator.Application.Interfaces;
using DominexRateOrchestrator.Application.Services;
using DominexRateOrchestrator.Domain.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace DominexRateOrchestrator.Tests
{
    public class ExchangeAggregatorServiceTests
    {
        [Fact]
        public async Task GetBestRateAsync_Should_Return_Highest_ConvertedAmount()
        {
            // Arrange
            var request = new ExchangeRequest
            {
                SourceCurrency = "USD",
                TargetCurrency = "DOP",
                Amount = 100
            };

            var mockProvider1 = new Mock<IExchangeRateProvider>();
            mockProvider1.Setup(p => p.GetRateAsync(request)).ReturnsAsync(new ExchangeResponse
            {
                Provider = "A",
                Rate = 57,
                ConvertedAmount = 5700
            });

            var mockProvider2 = new Mock<IExchangeRateProvider>();
            mockProvider2.Setup(p => p.GetRateAsync(request)).ReturnsAsync(new ExchangeResponse
            {
                Provider = "B",
                Rate = 59,
                ConvertedAmount = 5900
            });

            var service = new ExchangeAggregatorService(new[] {
                mockProvider1.Object,
                mockProvider2.Object
            });

            // Act
            var result = await service.GetBestRateAsync(request);

            // Assert
            result.Should().NotBeNull();
            result!.Provider.Should().Be("B");
            result!.ConvertedAmount.Should().Be(5900);
        }
    }
}
