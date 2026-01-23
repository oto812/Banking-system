using BankingSystem.Application.Common;
using BankingSystem.Application.DTOs.Reports;
using BankingSystem.Application.Interfaces;
using Domain.Enums;
using Domain.Interfaces;

namespace BankingSystem.Application.Services;

public class ReportService : IReportService
{
    private readonly IUserRepository _userRepository;
    private readonly ITransactionRepository _transactionRepository;

    public ReportService(IUserRepository userRepository, ITransactionRepository transactionRepository)
    {
        _userRepository = userRepository;
        _transactionRepository = transactionRepository;
    }
    
    public async Task<UserStatisticsDto> GetUserStatistics(CancellationToken ct)
    {
        
        var userStatistics = new UserStatisticsDto();
        var allUsers = await _userRepository.GetAllUsersAsync(ct);
        if (allUsers == null)
        {
            return userStatistics;
        }
        userStatistics.RegisteredLast30Days = allUsers.Where(u => u.RegistrationDate >= DateTime.Now.AddDays(-30)).Count();
        userStatistics.RegisteredThisYear = allUsers.Where(u => u.RegistrationDate.Year == DateTime.Now.Year).Count();
        userStatistics.RegisteredLastYear = allUsers.Where(u => u.RegistrationDate.Year == DateTime.Now.Year-1).Count();
        return userStatistics;
    }

    public async Task<TransactionStatisticsDto> GetTransactionStatistics(TimeRange timeRange, CancellationToken ct)
    {
        var transactionStatistics = new TransactionStatisticsDto();
        DateTime startDate;
        switch (timeRange)
        {
            case TimeRange.LastMonth:
                startDate = DateTime.Now.AddMonths(-1);
                break;
            case TimeRange.LastSixMonths:
                startDate = DateTime.Now.AddMonths(-6);
                break;
            case TimeRange.LastYear:
                startDate = DateTime.Now.AddYears(-1);
                break; 
            default:
                startDate = DateTime.Now;
                break;
        }

        var transactions = await _transactionRepository.GetTransactionsByDateRangeAsync(startDate, DateTime.Now,ct);
        int totalTransactions = transactions.Count();
        decimal incomeInGel = transactions.Where(t => t.Currency == Currency.GEL).Sum(t => t.Commission);
        decimal incomeInUsd = transactions.Where(t => t.Currency == Currency.USD).Sum(t => t.Commission);
        decimal incomeInEur = transactions.Where(t => t.Currency == Currency.EUR).Sum(t => t.Commission);
        decimal totalIncome = CurrencyConverter.Convert(incomeInUsd,Currency.USD, Currency.GEL) +
                              CurrencyConverter.Convert(incomeInEur,Currency.EUR, Currency.GEL) + incomeInGel;
        decimal averageIncomeGel = totalTransactions != 0 ? totalIncome / totalTransactions : 0;
        var lastMonthDailyCount = await GetDailyTransactionCounts(ct);

        transactionStatistics.TotalCount = totalTransactions;
        transactionStatistics.TotalIncomeGel = incomeInGel;
        transactionStatistics.TotalIncomeUsd = incomeInUsd;
        transactionStatistics.TotalIncomeEur = incomeInEur;
        transactionStatistics.AverageIncomeGel = averageIncomeGel;
        transactionStatistics.LastMonthDailyCounts = lastMonthDailyCount;
        return transactionStatistics;

    }

    private async Task<List<DailyTransactionCountDto>> GetDailyTransactionCounts(CancellationToken ct)
    {
        var last30DaysTransactions = await _transactionRepository.GetTransactionsByDateRangeAsync(DateTime.Now.AddDays(-30), DateTime.Now,ct);
        return last30DaysTransactions.GroupBy(t => t.TransactionDate.Date).Select(g => new DailyTransactionCountDto
        {
            Date = g.Key,
            Count = g.Count()
        }).ToList();
    }
    
    

    public async Task<decimal> GetTotalWithdrawalGELAsync(CancellationToken ct)
    {
        var transactions = await _transactionRepository.GetAllTransactionsAsync(ct);
        var withdrawals = transactions.Where(t => t.Type == TransactionType.Withdraw);
        return withdrawals.Sum(t => CurrencyConverter.Convert(t.Amount, t.Currency, Currency.GEL));
    }
}