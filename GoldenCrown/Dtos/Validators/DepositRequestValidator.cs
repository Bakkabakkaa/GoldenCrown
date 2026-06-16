using FluentValidation;
using GoldenCrown.Dtos.Finance;

namespace GoldenCrown.Dtos.Validators;

public class DepositRequestValidator : AbstractValidator<DepositRequest>
{
    public DepositRequestValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("The amount must be greater than 0");
    }
}