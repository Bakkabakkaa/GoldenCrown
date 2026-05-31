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
}