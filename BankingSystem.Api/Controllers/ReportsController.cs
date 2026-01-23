using BankingSystem.Application.DTOs.Reports;
using BankingSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Manager")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;
    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }
    [HttpGet("user-stats")]
    public async Task<IActionResult> GetUserStats(CancellationToken cancellationtoken)
    {
        
        var userStatistics = await _reportService.GetUserStatistics(cancellationtoken); 
        return Ok(userStatistics);
        
    }
    
    [HttpGet("transaction-stats")]
    public async Task<IActionResult> GetTransactionStats([FromQuery] TimeRange range, CancellationToken cancellationtoken)
    {
        var stats = await _reportService.GetTransactionStatistics(range,cancellationtoken);
        return Ok(stats);
    }
    
}