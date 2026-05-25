using GoldenCrown.Database;
using GoldenCrown.Models;
using Microsoft.EntityFrameworkCore;

namespace GoldenCrown.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<bool> RegisterAsync(string login, string name, string password)
    {
        var existing = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        if (existing != null)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
        {
            return false;
        }

        var user = new User
        {
            Login = login,
            Name = name,
            Password = password
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return true;
    }
}