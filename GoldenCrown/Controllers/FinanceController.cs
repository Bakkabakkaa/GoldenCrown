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
}