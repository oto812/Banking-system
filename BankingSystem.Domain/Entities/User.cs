
using Domain.Enums;

namespace Domain.Entities;

public class User
{
    public UserRole Role { get; set; }
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PrivateNumber { get; set; }
    public DateTime BirthDate { get; set; }
    
    public DateTime RegistrationDate { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public List<Account> Accounts { get; set; } = new();


}