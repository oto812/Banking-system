using BankingSystem.Application.DTOs.Account;
using BankingSystem.Application.DTOs.Card;
using BankingSystem.Application.DTOs.User;
using Domain.Entities;

namespace BankingSystem.Application.Interfaces;

public interface IOperatorService
{
    Task RegisterUserAsync(UserRegistrationDto userDto, CancellationToken cancellationToken);
    
    Task CreateAccountAsync(AccountCreateDto accountDto, CancellationToken cancellationToken);
    Task CreateCardAsync(CardCreateDto cardDto, CancellationToken cancellationToken);
}