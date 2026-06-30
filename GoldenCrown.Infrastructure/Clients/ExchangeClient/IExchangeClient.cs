using GoldenCrown.Infrastructure.Clients.ExchangeClient.Models;

namespace GoldenCrown.Infrastructure.Clients.ExchangeClient;

public interface IExchangeClient
{
    Task<decimal> GetExchangeRate(string baseCurrencyCode, string targetCurrencyCode, CancellationToken ct);
    Task<ExchangeRateResponse[]> GetExchangeRates(string baseCurrencyCode, CancellationToken ct);
}