using Domain.Enums;

namespace BankingSystem.Application.DTOs.Account;
public class AccountCreateDto
{
    public int UserId { get; set; }
    public string Iban { get; set; }
    public decimal InitialAmount { get; set; }
    public Currency Currency { get; set; }
    
}