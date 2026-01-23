using BankingSystem.Application.DTOs.Card;
using FluentValidation;

namespace BankingSystem.Application.Validators;

public class CardCreateValidator : AbstractValidator<CardCreateDto>
{
    public CardCreateValidator()
    {
        RuleFor(x => x.CardNumber)
            .Length(12)
            .Matches(@"^\d{12}$")
            .WithMessage("Card number must be 12 digits.");
        RuleFor(x => x.FullName).MaximumLength(30);
        RuleFor(x => x.ExpirationDate).Must(dob => dob > DateTime.Now)
            .WithMessage("Expiration date must be in the future.");
        RuleFor(x => x.Pin)
            .Matches(@"^\d{4}$")
            .WithMessage("Pin must be 4 digits.");
        RuleFor(x => x.Cvv)
            .Matches(@"^\d{3}$")
            .WithMessage("CVV must be 3 digits.");
    }
    
}