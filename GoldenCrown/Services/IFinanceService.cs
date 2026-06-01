using GoldenCrown.Dtos.Finance;

namespace GoldenCrown.Services;

public interface IFinanceService
{
    Task<Result<decimal>> GetBalanceAsync(string token);
    Task<Result> DepositAsync(string token, decimal amount);
    Task<Result> TransferAsync(string fromToken, string toLogin, decimal amount);

    Task<Result<List<TransactionHistoryResponse>>> GetTransactionHistoryAsync(string token,
        DateTime? dateFrom, DateTime? dateTo, int skip, int take);
}