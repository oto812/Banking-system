using Domain.Entities;

namespace Domain.Interfaces;

public interface ITransactionRepository
{
    public  Task AddTransactionAsync(Transaction transaction, CancellationToken ct);
    
    public Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken ct);
    
    public Task<IEnumerable<Transaction>> GetAllTransactionsAsync(CancellationToken ct);
}