using GoldenCrown.Database;
using GoldenCrown.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoldenCrown.Features.User.UserRegister;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, Result>
{
    private readonly ApplicationDbContext _context;

    public UserRegisterCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var existing = await _context.Users.FirstOrDefaultAsync(u => u.Login == request.Login);
        if (existing != null)
        {
            return Result.Failure("User already exists");
        }

        var user = new Models.User
        {
            Login = request.Login,
            Name = request.Name,
            Password = request.Password
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var currencies = new List<string>
        {
            Currency.GBP,
            Currency.USD,
            Currency.EUR
        };
            
        foreach (var currency in currencies)
        {
            var account = new Account()
            {
                UserId = user.Id,
                Balance = 0,
                Currency = currency
            };
            
            _context.Accounts.Add(account);
        }
        
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}