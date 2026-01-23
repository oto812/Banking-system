using BankingSystem.Application.DTOs.Account;
using BankingSystem.Application.DTOs.Card;
using BankingSystem.Application.DTOs.User;
using BankingSystem.Application.Interfaces;
using BankingSystem.Application.Validators;
using Domain.Entities;
using Domain.Interfaces;

namespace BankingSystem.Application.Services;

public class OperatorService : IOperatorService
{
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ICardRepository _cardRepository;

    public OperatorService(IUserRepository userRepository, IAccountRepository accountRepository,
        ICardRepository cardRepository)
    {
        _userRepository = userRepository;
        _accountRepository = accountRepository;
        _cardRepository = cardRepository;
    }


    public async Task RegisterUserAsync(UserRegistrationDto userDto, CancellationToken ct)
    {
        var validation = new UserRegistrationValidator();
        var validationResult = await validation.ValidateAsync(userDto);

        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }
        var existsUser = await _userRepository.GetUserByPrivateNumberAsync(userDto.PrivateNumber,ct);

        if (existsUser != null)
        {
            throw new Exception("User already exists");
        }

        var newUser = new User
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            PrivateNumber = userDto.PrivateNumber,
            Email = userDto.Email,
            BirthDate = userDto.DateOfBirth,
            RegistrationDate = DateTime.Now,
            PasswordHash = userDto.Password
        };
        
        await _userRepository.AddUserAsync(newUser,ct);

    }

    public async Task CreateAccountAsync(AccountCreateDto accountDto, CancellationToken ct)
    {
        var validation = new AccountCreateValidator();
        var validationResult = await validation.ValidateAsync(accountDto);
        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }
        if (accountDto.Iban.Length != 22)
        {
            throw new Exception("Invalid IBAN length");
        }

        var accountOwner = await _userRepository.GetUserByIdAsync(accountDto.UserId, ct);
        if (accountOwner == null)
        {
            throw new Exception("Account owner not found");
        }

        var accountExists = await _accountRepository.GetAccountByIbanAsync(accountDto.Iban,ct);
        if (accountExists != null)
        {
            throw new Exception("Account already exists");
        }

        var newAccount = new Account
        {
            UserId = accountDto.UserId,
            Iban = accountDto.Iban,
            Balance = accountDto.InitialAmount,
            Currency = accountDto.Currency,
            Cards = new List<Card>()
        };
        await _accountRepository.AddAccountAsync(newAccount,ct);
        


    }

    public async Task CreateCardAsync(CardCreateDto cardDto, CancellationToken ct)
    {
        var validation = new CardCreateValidator();
        var validationResult = await validation.ValidateAsync(cardDto);
        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }
        var accountExists = await _accountRepository.GetAccountByIdAsync(cardDto.AccountId, ct);
        if (accountExists == null)
        {
            throw new Exception("Account not found");
        }
        
        var newCard = new Card
        {
            AccountId = cardDto.AccountId,
            CardNumber = cardDto.CardNumber,
            FullName = cardDto.FullName,
            ExpiryDate = cardDto.ExpirationDate,
            CvvHash = cardDto.Cvv,
            PinHash = cardDto.Pin
            
        };
        await _cardRepository.AddCardAsync(newCard, ct);
        
        
    }
}