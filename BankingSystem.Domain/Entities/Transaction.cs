using Domain.Enums;

namespace Domain.Entities;

public class Transaction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public int SourceAccountId { get; set; }
    public int DestinationAccountId { get; set; }
    public TransactionType Type { get; set; }
    public decimal Commission { get; set; }
    public Currency Currency { get; set; }

    public Transaction()
    {
        
    }
    public Transaction(decimal amount, DateTime transactionDate,int sourceAccountId,int destinationAccountId, TransactionType type, Currency currency,
        decimal commission)
    {
        Amount = amount;
        TransactionDate = transactionDate;
        SourceAccountId = sourceAccountId;
        DestinationAccountId = destinationAccountId;
        Type = type;
        Commission = commission;
        Currency = currency;
    }
}