using BankingSystem.Application.DTOs.Atm;
using BankingSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService)
    {
        _atmService = atmService;
    }

    [HttpPost("balance")]
    public async Task<IActionResult> GetBalance(AtmLoginDto dto, CancellationToken cancellationToken)
    {
        var balance = await _atmService.GetBalanceAsync(dto,cancellationToken); 
        return Ok(balance);
       
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw(AtmWithdrawDto dto, CancellationToken cancellationToken)
    {
     
        await _atmService.WithdrawAsync(dto,cancellationToken);
        return Ok();
        
    }

    [HttpPost("change-pin")]
    public async Task<IActionResult> ChangePin(AtmChangePinDto dto, CancellationToken cancellationToken)
    {
        await _atmService.ChangePinAsync(dto,cancellationToken); 
        return Ok();
    }
    
    
   
    
    
    
}