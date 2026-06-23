using GoldenCrown.Domain.Models;
using GoldenCrown.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoldenCrown.Application.Features.Finance.Transfer;

public class TransferCommandHandler : IRequestHandler<TransferCommand, Result>
{
    private readonly ApplicationDbContext _context;

    public TransferCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Result> Handle(TransferCommand request, CancellationToken cancellationToken)
    {
        var fromAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == request.FromUserId && a.Currency == request.Currency, cancellationToken);

        if (fromAccount == null)
        {
            return Result.Failure("Account not found");
        }
        
        var toUser = await _context.Users.FirstOrDefaultAsync(u => u.Login == request.ToLogin, cancellationToken);

        if (toUser == null)
        {
            return Result.Failure("Recipient not found");
        }

        var toAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == toUser.Id && a.Currency == request.Currency, cancellationToken);

        if (toAccount == null)
        {
            return Result.Failure("Account not found");
        }
        
        if (fromAccount.Balance < request.Amount)
        {
            return Result.Failure("Insufficient funds");
        }

        fromAccount.Balance -= request.Amount;
        toAccount.Balance += request.Amount;

        var transaction = new Transaction()
        {
            ReceiverAccountId = toAccount.Id,
            SenderAccountId = fromAccount.Id,
            Amount = request.Amount,
            CreatedAt = DateTime.UtcNow,
            Currency = request.Currency
        };

        _context.Transactions.Add(transaction);

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}