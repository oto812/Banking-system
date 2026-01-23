using BankingSystem.Infrastructure.Persistence;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Infrastructure.Repositories;

public class CardRepository : ICardRepository
{
    private readonly AppDbContext _appDbContext;

    public CardRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<Card?> GetCardbyCardNumberAsync(string cardNumber, CancellationToken ct)
    {
        return await _appDbContext.Cards.SingleOrDefaultAsync(c => c.CardNumber == cardNumber,ct);
    }

    public async Task AddCardAsync(Card card, CancellationToken ct)
    {
        await _appDbContext.Cards.AddAsync(card,ct);
        await _appDbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateCardAsync(Card card, CancellationToken ct)
    {
        _appDbContext.Cards.Update(card);
        await _appDbContext.SaveChangesAsync(ct);
    }
}