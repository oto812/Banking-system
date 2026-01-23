namespace BankingSystem.Application.DTOs.Reports;

public class UserStatisticsDto
{
    public int RegisteredThisYear { get; set; }
    public int RegisteredLastYear { get; set; }
    public int RegisteredLast30Days { get; set; }
}