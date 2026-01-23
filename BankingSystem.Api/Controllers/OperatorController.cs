using BankingSystem.Application.DTOs.Account;
using BankingSystem.Application.DTOs.Card;
using BankingSystem.Application.DTOs.User;
using BankingSystem.Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Operator")]
public class OperatorController : ControllerBase
{
    private readonly IOperatorService _operatorService;

    public OperatorController(IOperatorService operatorService)
    {
        _operatorService = operatorService;
    }

    [HttpPost("user")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto dto, CancellationToken cancellationToken)
    {
        
        await _operatorService.RegisterUserAsync(dto, cancellationToken); 
        return Ok(new { message = "User registered successfully" });
        
    }

    [HttpPost("Account")]
    public async Task<IActionResult> CreateAccount([FromBody]  AccountCreateDto dto, CancellationToken cancellationToken)
    {
        await _operatorService.CreateAccountAsync(dto, cancellationToken); 
        return Ok(new { message = "Account created successfully" });
        
    }

    public async Task<IActionResult> RegisterCard([FromBody] CardCreateDto dto , CancellationToken cancellationToken)
    {
        
            _operatorService.CreateCardAsync(dto, cancellationToken);
            return Ok(new { message = "Card created successfully" });
            
    }
    
}