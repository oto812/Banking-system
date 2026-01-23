using Domain.Entities;

namespace Domain.Interfaces;

public interface ICardRepository
{
    public Task<Card?> GetCardbyCardNumberAsync(string cardNumber, CancellationToken ct);
    
    public Task AddCardAsync(Card card, CancellationToken ct);
    
    public Task UpdateCardAsync(Card card, CancellationToken ct);
    
}