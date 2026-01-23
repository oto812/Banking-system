using System.ComponentModel.DataAnnotations;
using BankingSystem.Application.Common;
using BankingSystem.Application.DTOs.Atm;
using BankingSystem.Application.Interfaces;
using BankingSystem.Application.Validators;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace BankingSystem.Application.Services;

public class AtmService : IAtmService
{
    private readonly ICardRepository _cardRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;

    public AtmService(ICardRepository cardRepository, IAccountRepository accountRepository, ITransactionRepository transactionRepository)
    {
        _cardRepository = cardRepository;
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
    }
    private async Task<Card> AuthorizeCardAsync(string cardNumber, string pin, CancellationToken cancellationToken)
    {
        
        var card = await _cardRepository.GetCardbyCardNumberAsync(cardNumber,cancellationToken);
        if (card == null)
        {
            throw new ApplicationException("card number not found");
        }
        if (card.PinHash != pin)
        {
            throw new ApplicationException("card pin hash does not match");
        }
        if (card.ExpiryDate < DateTime.Now)
        {
            throw new Exception("This card has expired.");
        }
        return card;
        
    }

    public async Task<decimal> GetBalanceAsync(AtmLoginDto atmLoginDto, CancellationToken cancellationToken)
    {
        var validation = new AtmLoginValidator();
        var validationResult = await validation.ValidateAsync(atmLoginDto);

        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }
        
        var card = await AuthorizeCardAsync(atmLoginDto.CardNumber, atmLoginDto.Pin, cancellationToken);
        var account = await _accountRepository.GetAccountByIdAsync(card.AccountId, cancellationToken);
        if (account == null)
        {
            throw new Exception("Account not found");
        }
        return account.Balance;
    }

    public async Task ChangePinAsync(AtmChangePinDto atmChangePinDto, CancellationToken cancellationToken)
    {
        var validator = new AtmChangePinValidator();
        var validationResult = await validator.ValidateAsync(atmChangePinDto);

        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }
        var card = await AuthorizeCardAsync(atmChangePinDto.CardNumber, atmChangePinDto.Pin, cancellationToken);
        if (card == null)
        {
            throw new Exception("card doesn't exist");
        }
        if (atmChangePinDto.NewPin.Length != 4)
        {
            throw new Exception("PIN must be 4 digits.");
        }
        card.PinHash = atmChangePinDto.NewPin;
        await _cardRepository.UpdateCardAsync(card,cancellationToken);
        
    }

    public async Task WithdrawAsync(AtmWithdrawDto atmWithdrawDto, CancellationToken cancellationToken)
    {
        var validator = new AtmWithdrawValidator();
        var validationResult = await validator.ValidateAsync(atmWithdrawDto);
        
        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }

        
        var card = await AuthorizeCardAsync(atmWithdrawDto.CardNumber, atmWithdrawDto.Pin, cancellationToken);
        
        
        var account = await _accountRepository.GetAccountByIdAsync(card.AccountId, cancellationToken);
        
        decimal balanceInGel = CurrencyConverter.Convert(account.Balance, account.Currency,Currency.GEL);
        decimal withdrawAmountInGel = CurrencyConverter.Convert(atmWithdrawDto.Amount, atmWithdrawDto.Currency, Currency.GEL);
        if (balanceInGel < withdrawAmountInGel)
        {
            throw new ApplicationException("Insufficient balance");
        }
        await CheckDailyLimitAsync(account,atmWithdrawDto.Amount,atmWithdrawDto.Currency, cancellationToken);
        
        decimal commissionValue = atmWithdrawDto.Amount * 0.02m;
        
        decimal deductFromBalance = CurrencyConverter.Convert(atmWithdrawDto.Amount, atmWithdrawDto.Currency, account.Currency);
        decimal deductCommissionFromBalance = CurrencyConverter.Convert(commissionValue, atmWithdrawDto.Currency, account.Currency);
        account.Balance -= deductFromBalance + deductCommissionFromBalance;
        
        await _accountRepository.UpdateAccountAsync(account,cancellationToken);

        var transaction = new Transaction
        {
            Amount = atmWithdrawDto.Amount,
            TransactionDate = DateTime.Now,
            SourceAccountId = account.Id,
            Type = TransactionType.Withdraw,
            Commission = commissionValue,
            Currency = atmWithdrawDto.Currency,
        };
        
        await _transactionRepository.AddTransactionAsync(transaction,cancellationToken);
        


    }

    private async Task CheckDailyLimitAsync(Account account, decimal requestedAmount, Currency currency, CancellationToken cancellationToken)
    {
       var from = DateTime.Now.AddDays(-1);
       var to = DateTime.Now;

       var transactions = await _transactionRepository.GetTransactionsByDateRangeAsync(from, to, cancellationToken);
       
       var dailyWithdrawTransactions = transactions.Where(t => t.SourceAccountId == account.Id && t.Type == TransactionType.Withdraw);

       decimal totalInGel = 0;
       foreach(var t in dailyWithdrawTransactions)
       {
           totalInGel += CurrencyConverter.Convert(t.Amount, t.Currency, Currency.GEL);
       }
       totalInGel += CurrencyConverter.Convert(requestedAmount, currency, Currency.GEL);
       if (totalInGel > 10000)
       {
           throw new Exception("Daily limit 10000 GEL reached");
       }
    }
   
}