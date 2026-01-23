namespace BankingSystem.Application.Common;
using Domain.Enums;
public static class CurrencyConverter
{
    public static decimal Convert(decimal Amount, Currency from, Currency to)
    {
        switch (from,to)
        {
            case (Currency.USD,Currency.GEL):
                return Amount * (decimal)2.7;
            case (Currency.GEL,Currency.USD):
                return Amount / (decimal)2.7;
            case (Currency.EUR,Currency.GEL):
                return Amount * (decimal)3.1;
            case (Currency.GEL,Currency.EUR):
                return Amount / (decimal)3.1;
            case (Currency.USD,Currency.EUR):
                return Amount * (decimal)0.86;
            case (Currency.EUR,Currency.USD):
                return Amount / (decimal)0.86;
            default:
                return Amount;
            
        }
    }
}