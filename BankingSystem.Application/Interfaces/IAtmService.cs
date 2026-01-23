using BankingSystem.Application.DTOs.Atm;
using Domain.Entities;

namespace BankingSystem.Application.Interfaces;

public interface IAtmService
{
    
    Task<decimal> GetBalanceAsync(AtmLoginDto atmLoginDto, CancellationToken cancellationToken);
    
    Task ChangePinAsync(AtmChangePinDto atmLoginDto, CancellationToken cancellationToken);
    
    Task WithdrawAsync(AtmWithdrawDto atmWithdrawDto, CancellationToken cancellationToken);
}