using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(int id, CancellationToken ct);
    
    Task<IEnumerable<User>?> GetAllUsersAsync(CancellationToken ct);
    
    Task<User?> GetUserByPrivateNumberAsync(string privateNumber, CancellationToken ct);
    
    Task AddUserAsync(User user, CancellationToken ct);
    Task<User?> GetUserByEmailAsync(string email, CancellationToken ct);
    
}