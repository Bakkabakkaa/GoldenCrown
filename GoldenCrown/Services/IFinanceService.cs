namespace GoldenCrown.Services;

public interface IFinanceService
{
    Task<Result<decimal>> GetBalance(string token);
}