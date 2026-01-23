using BankingSystem.Application.DTOs.Transfer;

namespace BankingSystem.Application.Interfaces;

public interface IUserService
{
    Task TransferMoneyAsync(TransferDto transferDto, CancellationToken cancellationToken);
}