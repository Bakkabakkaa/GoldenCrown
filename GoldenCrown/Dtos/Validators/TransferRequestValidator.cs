using FluentValidation;
using GoldenCrown.Dtos.Finance;
using GoldenCrown.Models;

namespace GoldenCrown.Dtos.Validators;

public class TransferRequestValidator : AbstractValidator<TransferRequest>
{
    public TransferRequestValidator()
    {
        RuleFor(x => x.ReceiverLogin)
            .NotEmpty().WithMessage("The 'ReceiverLogin' field is required");
        
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("The amount must be greater than 0");
        
        RuleFor(x => x.Currency)
            .NotEmpty()
            .Must(currency => (new List<string>(){Currency.USD, Currency.EUR, Currency.GBP}).Contains(currency))
            .WithMessage("Specify the currency");
    }
}