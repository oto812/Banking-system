using BankingSystem.Infrastructure.Persistence;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _appDbContext;

    public TransactionRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task AddTransactionAsync(Transaction transaction, CancellationToken ct)
    {
        await _appDbContext.Transactions.AddAsync(transaction,ct);
        await _appDbContext.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken ct)
    {
        return await _appDbContext.Transactions.
            Where(t =>  startDate <= t.TransactionDate && t.TransactionDate < endDate)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync(CancellationToken ct)
    {
        return await _appDbContext.Transactions.
            AsNoTracking()
            .ToListAsync(ct);
    }
}