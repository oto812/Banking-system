using BankingSystem.Application.DTOs.Atm;
using FluentValidation;

namespace BankingSystem.Application.Validators;

public class AtmWithdrawValidator : AbstractValidator<AtmWithdrawDto>
{
    public AtmWithdrawValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("you cannot withdraw 0 or negative money");
        RuleFor(x => x.Pin)
            .Length(4)
            .Matches(@"^\d{4}$").WithMessage("Pin must be exactly 4 digits");
        RuleFor(x => x.CardNumber)
            .Length(12)
            .Matches(@"^\d{12}$")
            .WithMessage("Card number must be exactly 12 characters");
        
    }
    
}