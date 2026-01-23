
using Domain.Entities;
using Domain.Enums;

namespace Domain.Entities;

public class Account
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Iban { get; set; }
    public decimal Balance { get; set; }
    public Currency Currency { get; set; }
    public List<Card> Cards { get; set; } = new ();
    
    
}