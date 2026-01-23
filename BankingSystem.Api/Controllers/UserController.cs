using BankingSystem.Application.DTOs.Transfer;
using BankingSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> TransferMoney(TransferDto dto, CancellationToken cancellationToken)
    {
        
        await _userService.TransferMoneyAsync(dto, cancellationToken);
        return Ok();
        
       
    }
    
}