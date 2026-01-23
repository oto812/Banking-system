using BankingSystem.Application.DTOs.User;
using FluentValidation;

namespace BankingSystem.Application.Validators;

public class UserRegistrationValidator : AbstractValidator<UserRegistrationDto>
{
    public UserRegistrationValidator()
    {
        RuleFor(x => x.FirstName).MaximumLength(15).NotEmpty();
        RuleFor(x => x.LastName).MaximumLength(15).NotEmpty();
        RuleFor(x => x.PrivateNumber).Matches(@"^\d{11}$");
        RuleFor(x => x.DateOfBirth).Must(dob => dob < DateTime.Now.AddYears(-16));
        RuleFor(x => x.Email).Must(email => email.Contains("@"));
        RuleFor(x => x.Password)
            .MinimumLength(8)
            .WithMessage("password must be at least 8 characters long")
            .Must(p => p.Any(char.IsUpper))
            .WithMessage("Password must contain at least on uppercase letter")
            .Must(p => p.Any(char.IsLower))
            .WithMessage("Password must contain at least on lowercase letter")
            .Must(p => p.Any(char.IsDigit))
            .WithMessage("Password must contain at least on digit number")
            .Must(p => p.Any(ch => !char.IsLetterOrDigit(ch)));


    }
}