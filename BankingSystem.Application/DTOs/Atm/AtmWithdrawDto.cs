namespace BankingSystem.Application.DTOs.Atm;
using Domain.Enums;
public class AtmWithdrawDto
{
    public string CardNumber { get; set; }
    public string Pin { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
}