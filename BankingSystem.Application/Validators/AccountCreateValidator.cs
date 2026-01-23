using BankingSystem.Application.DTOs.Account;
using FluentValidation;

namespace BankingSystem.Application.Validators;

public class AccountCreateValidator : AbstractValidator<AccountCreateDto>
{
    public AccountCreateValidator()
    {
        RuleFor(x => x.InitialAmount)
            .GreaterThan(0)
            .WithMessage("Initial amount must be greater than 0");
        RuleFor(x => x.Iban)
            .Matches(@"^[A-Z]{2}\d{2}[A-Z0-9]{11,30}$")
            .WithMessage("Invalid IBAN");
    }
}