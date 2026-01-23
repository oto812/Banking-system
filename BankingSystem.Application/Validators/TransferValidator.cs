using BankingSystem.Application.DTOs.Transfer;
using FluentValidation;

namespace BankingSystem.Application.Validators;

public class TransferValidator : AbstractValidator<TransferDto>
{
    public TransferValidator()
    {
        RuleFor(x => x.DestinationIban)
            .NotEmpty()
            .Matches(@"^[A-Z]{2}\d{2}$[A-Z0-9]{11,30}]")
            .WithMessage("Invalid destination IBAN format");
        RuleFor(x => x.SourceIban)
            .NotEmpty()
            .Matches(@"^[A-Z]{2}\d{2}$[A-Z0-9]{11,30}]")
            .WithMessage("Invalid destination IBAN format");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0");
    }
}