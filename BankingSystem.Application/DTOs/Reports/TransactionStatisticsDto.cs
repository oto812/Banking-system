namespace BankingSystem.Application.DTOs.Reports;

public class TransactionStatisticsDto
{
    public int TotalCount { get; set; }
    public decimal TotalIncomeGel { get; set; }
    public decimal TotalIncomeUsd { get; set; }
    public decimal TotalIncomeEur { get; set; }
    public decimal AverageIncomeGel { get; set; }
    
    public List<DailyTransactionCountDto> LastMonthDailyCounts { get; set; }
}