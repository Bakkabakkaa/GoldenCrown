using FluentValidation;
using GoldenCrown.Api.Dtos.Finance;

namespace GoldenCrown.Api.Validators;

public class TransactionHistoryRequestValidator : AbstractValidator<TransactionHistoryRequest>
{
    public TransactionHistoryRequestValidator()
    {
        RuleFor(x => x.Limit)
            .GreaterThan(1).WithMessage("The limit value must be at least 1.");

        RuleFor(x => x.Offset)
            .GreaterThanOrEqualTo(0).WithMessage("The offset value cannot be negative.");
    }
}