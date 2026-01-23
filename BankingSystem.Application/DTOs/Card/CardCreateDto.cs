namespace BankingSystem.Application.DTOs.Card;

public class CardCreateDto
{
    public int AccountId { get; set; }
    public string CardNumber { get; set; }
    public string FullName { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Cvv {get; set;}
    public string Pin { get; set; }
}