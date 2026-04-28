using Microsoft.AspNetCore.Mvc;
using FinTrack.API.DTOs;
using FinTrack.API.Interfaces;

namespace FinTrack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    // AuthService'i DI ile alıyoruz
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // POST /api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);

        if (result == "Bu email zaten kayıtlı")
            return BadRequest(new { message = result });

        return Ok(new { message = result });
    }

    // POST /api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);

        if (result == null)
            return Unauthorized(new { message = "Email veya şifre hatalı" });

        return Ok(new { message = result });
    }
}