using BankingSystem.Application.DTOs.Reports;

namespace BankingSystem.Application.Interfaces;

public interface IReportService
{
    Task<UserStatisticsDto> GetUserStatistics(CancellationToken cancellationToken);
    
    Task<TransactionStatisticsDto> GetTransactionStatistics(TimeRange timeRange, CancellationToken cancellationToken);
    
    Task<decimal> GetTotalWithdrawalGELAsync(CancellationToken cancellationToken);
}