using System.Data;
using BankingSystem.Application.DTOs.Atm;
using FluentValidation;

namespace BankingSystem.Application.Validators;

public class AtmChangePinValidator : AbstractValidator<AtmChangePinDto>
{
    public AtmChangePinValidator()
    {
        RuleFor(x => x.CardNumber)
            .Matches(@"^\d{12}$")
            .WithMessage("Card number must be 12 digit number");
        RuleFor(x => x.Pin)
            .Matches(@"^\d{3}$")
            .WithMessage("Pin must be 3 digits");
        RuleFor(x => x.NewPin)
            .Matches(@"^\d{3}$")
            .WithMessage("New pin number must be 3 digits");
    }
    
}