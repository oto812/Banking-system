using BankingSystem.Application.Common;
using BankingSystem.Application.DTOs.Transfer;
using BankingSystem.Application.Interfaces;
using BankingSystem.Application.Validators;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Entities;
namespace BankingSystem.Application.Services;

public class UserService : IUserService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;

    public UserService(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task TransferMoneyAsync(TransferDto transferDto, CancellationToken ct)
    {
        var validation = new TransferValidator();
        var validationResult = await validation.ValidateAsync(transferDto);
        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }
        var sourceAccount = await _accountRepository.GetAccountByIbanAsync(transferDto.SourceIban,ct);
        var destinationAccount = await _accountRepository.GetAccountByIbanAsync(transferDto.DestinationIban,ct);

        if (sourceAccount == null || destinationAccount == null)
        {
            throw new Exception("Invalid source account or destination account");
        }

        if (transferDto.SourceIban == transferDto.DestinationIban)
        {
            throw new Exception("source and destination accounts are the same");
        }

        decimal commission = sourceAccount.UserId == destinationAccount.UserId ? 
            0 : transferDto.Amount * 0.01m + 0.5m;
        decimal amountToTransfer = transferDto.Amount + commission;
        if (sourceAccount.Balance < amountToTransfer)
        {
            throw new Exception("Insufficient funds");
        }
        decimal amountToRecieve = sourceAccount.Currency != destinationAccount.Currency ? 
            CurrencyConverter.Convert(transferDto.Amount, sourceAccount.Currency, destinationAccount.Currency) :
            transferDto.Amount;
        sourceAccount.Balance -= amountToTransfer;
        destinationAccount.Balance += amountToRecieve;
        await _accountRepository.UpdateAccountAsync(sourceAccount,ct);
        await _accountRepository.UpdateAccountAsync(destinationAccount,ct);

        var transaction = new Transaction(
            transferDto.Amount,
            DateTime.Now,
            sourceAccount.Id,
            destinationAccount.Id,
            TransactionType.Transfer,
            sourceAccount.Currency, 
            commission
        );
        
        await _transactionRepository.AddTransactionAsync(transaction,ct);
        
    }
}