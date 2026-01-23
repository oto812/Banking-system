namespace BankingSystem.Application.DTOs.Transfer;

public class TransferDto
{
    public string SourceIban { get; set; }
    public string DestinationIban { get; set; }
    public decimal Amount { get; set; }
}