using BankingSystem.Infrastructure.Persistence;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Infrastructure.Repositories;

public class UserRepository:IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<User?> GetUserByIdAsync(int id, CancellationToken ct)
    {
        return await _appDbContext.Users.FindAsync(id,ct);
    }

    public async Task<IEnumerable<User>?> GetAllUsersAsync(CancellationToken ct)
    {
        return await _appDbContext.Users.
            ToListAsync(ct);
    }

    public async Task<User?> GetUserByPrivateNumberAsync(string privateNumber, CancellationToken ct)
    {
        return await _appDbContext.Users.FirstOrDefaultAsync(u => u.PrivateNumber == privateNumber,ct);
    }

    public async Task AddUserAsync(User user, CancellationToken ct)
    {
        await _appDbContext.Users.AddAsync(user,ct);
        await _appDbContext.SaveChangesAsync(ct);
    }

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken ct)
    {
        return await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email,ct);
    }
}