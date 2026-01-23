using BankingSystem.Application.DTOs.User;
using BankingSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto, CancellationToken cancellationToken)
    {
        var token = await _authService.LoginAsync(dto,cancellationToken);
        return Ok(new { Token = token });
    }
    
    
    
}