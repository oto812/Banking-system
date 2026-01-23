namespace BankingSystem.Application.DTOs.Atm;

public class AtmChangePinDto
{
    public string CardNumber { get; set; }
    public string Pin { get; set; }
    public string NewPin { get; set; }
}