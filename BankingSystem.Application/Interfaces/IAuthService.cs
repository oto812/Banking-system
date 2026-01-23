using BankingSystem.Application.DTOs.User;

namespace BankingSystem.Application.Interfaces;

public interface IAuthService
{
    ValueTask<string> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken);
}