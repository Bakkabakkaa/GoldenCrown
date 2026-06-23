using FluentValidation;
using GoldenCrown.Application.Dtos.User;

namespace GoldenCrown.Api.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("The login field is required")
            .MinimumLength(3).WithMessage("The minimum login length is 3 characters");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("The password field is required")
            .MinimumLength(6).WithMessage("The minimum login length is 6 characters");
    }
}