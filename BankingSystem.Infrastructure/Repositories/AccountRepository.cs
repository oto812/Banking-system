using BankingSystem.Infrastructure.Persistence;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _appDbContext;

    public AccountRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Account?> GetAccountByIdAsync(int id, CancellationToken ct)
    {
        return await _appDbContext.Accounts.Include(a => a.Cards).FirstOrDefaultAsync(a => a.Id == id,ct);
    }

    public async Task<Account?> GetAccountByIbanAsync(string iban, CancellationToken ct)
    {
        return await _appDbContext.Accounts.Include(a => a.Cards).FirstOrDefaultAsync(a => a.Iban == iban,ct);
    }

    public async Task<IEnumerable<Account>> GetAccountsByUserIdAsync(int userId, CancellationToken ct)
    {
        return await _appDbContext.Accounts.Include(a => a.Cards).Where(a => a.UserId == userId)
            .ToListAsync(ct);
    }

    public async Task AddAccountAsync(Account account, CancellationToken ct)
    {
        await _appDbContext.Accounts.AddAsync(account,ct);
        await _appDbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAccountAsync(Account account, CancellationToken ct)
    {
        _appDbContext.Accounts.Update(account);
        await _appDbContext.SaveChangesAsync(ct);
    }
}