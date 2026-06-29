using FluentValidation;
using GoldenCrown.Api.Dtos.Finance;
using GoldenCrown.Domain.Models;

namespace GoldenCrown.Api.Validators;

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
            .Must(currency => Currency.AllCurrencies.Contains(currency))
            .WithMessage("Specify the currency");

        RuleFor(x => x.ReceiverCurrency)
            .NotEmpty()
            .Must(currency => Currency.AllCurrencies.Contains(currency))
            .WithMessage("Specify the recipient's currency");
    }
}