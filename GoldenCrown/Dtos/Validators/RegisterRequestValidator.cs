using FluentValidation;
using GoldenCrown.Dtos.User;

namespace GoldenCrown.Dtos.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("The login field is required")
            .MinimumLength(3).WithMessage("The minimum login length is 3 characters");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The name field is required");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("The password field is required")
            .MinimumLength(6).WithMessage("Minimum password length is 6 characters");
    }
}