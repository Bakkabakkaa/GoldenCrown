namespace GoldenCrown.Services;

public interface IFinanceService
{
    Task<Result<decimal>> GetBalanceAsync(string token);
    Task<Result> DepositAsync(string token, decimal amount);
}