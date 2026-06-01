using GoldenCrown.Dtos.Finance;
using GoldenCrown.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoldenCrown.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FinanceController : ControllerBase
{
    private readonly IFinanceService _financeService;

    public FinanceController(IFinanceService financeService)
    {
        _financeService = financeService;
    }

    [HttpGet("balance")]
    public async Task<IActionResult> GetBalanceAsync([FromHeader]string token)
    {
        var balanceResult = await _financeService.GetBalanceAsync(token);

        if (balanceResult.IsSuccess)
        {
            return Ok(new BalanceResponse()
            {
                Balance = balanceResult.Value
            });
        }

        return BadRequest(new { Message = balanceResult.ErrorMessage });
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> DepositAsync([FromBody] DepositRequest depositRequest)
    {
        var depositResult = await _financeService.DepositAsync(depositRequest.Token, depositRequest.Amount);

        if (depositResult.IsSuccess)
        {
            return Ok();
        }

        return BadRequest(new { Message = depositResult.ErrorMessage });
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> TransferAsync([FromBody] TransferRequest transferRequest)
    {
        var transferResult = await _financeService.TransferAsync(transferRequest.Token,
            transferRequest.ReceiverLogin, transferRequest.Amount);

        if (transferResult.IsSuccess)
        {
            return Ok();
        }

        return BadRequest(new { Message = transferResult.ErrorMessage });
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetTransactionHistoryAsync(TransactionHistoryRequest request)
    {
        var historyResult = await _financeService.GetTransactionHistoryAsync(request.Token,
            request.From, request.To,
            request.Offset, request.Limit);

        if (historyResult.IsSuccess)
        {
            return Ok(historyResult.Value);
        }

        return BadRequest(new { Message = historyResult.ErrorMessage });
    }
}