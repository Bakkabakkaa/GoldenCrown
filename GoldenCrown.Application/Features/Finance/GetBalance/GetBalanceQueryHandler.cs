using GoldenCrown.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoldenCrown.Application.Features.Finance.GetBalance;

public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, Result<decimal>>
{
    private readonly ApplicationDbContext _context;

    public GetBalanceQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Result<decimal>> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == request.UserId 
                                                                       && a.Currency == request.Currency, cancellationToken);

        if (account == null)
        {
            return Result<decimal>.Failure("Account not found");
        }
        return Result<decimal>.Success(account.Balance);
    }
}