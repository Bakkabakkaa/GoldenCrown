namespace GoldenCrown.Application.Services.Currency;

public interface ICurrencyService
{
    ValueTask<decimal> Convert(decimal amount, string currencyCode, string targetCurrencyCode, CancellationToken ct);
}