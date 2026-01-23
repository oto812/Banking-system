namespace Domain.Entities;

public class Card
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public string CardNumber { get; set; }
    public string FullName { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string CvvHash { get; set; }
    public string PinHash { get; set; }
}