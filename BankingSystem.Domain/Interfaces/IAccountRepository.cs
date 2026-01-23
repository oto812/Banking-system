using Domain.Entities;

namespace Domain.Interfaces;

public interface IAccountRepository
{
    Task<Account?> GetAccountByIdAsync(int id, CancellationToken ct);
    Task<Account?> GetAccountByIbanAsync(string iban, CancellationToken ct);
    
    Task<IEnumerable<Account>> GetAccountsByUserIdAsync(int userId, CancellationToken ct);
    
    Task AddAccountAsync(Account account, CancellationToken ct);
    
    Task UpdateAccountAsync(Account account, CancellationToken ct);
}