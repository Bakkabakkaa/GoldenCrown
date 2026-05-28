namespace GoldenCrown.Services;

public interface IUserService
{
    Task<Result> RegisterAsync(string login, string name, string password);
    Task<Result<string>> Login(string login, string password);
}