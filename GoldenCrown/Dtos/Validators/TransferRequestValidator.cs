using FluentValidation;
using GoldenCrown.Dtos.Finance;

namespace GoldenCrown.Dtos.Validators;

public class TransferRequestValidator : AbstractValidator<TransferRequest>
{
    public TransferRequestValidator()
    {
        RuleFor(x => x.ReceiverLogin)
            .NotEmpty().WithMessage("The 'ReceiverLogin' field is required");
        
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("The amount must be greater than 0");
    }
}