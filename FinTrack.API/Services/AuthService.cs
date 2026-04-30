using Microsoft.EntityFrameworkCore;
using FinTrack.API.DTOs;
using FinTrack.API.Entities;
using FinTrack.API.Interfaces;
using FinTrack.API.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace FinTrack.API.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        AppDbContext context,
        ILogger<AuthService> logger)
    {
        _context = context;
        _logger = logger;
    }

    // REGISTER
    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        var email = dto.Email.Trim().ToLower();

        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        if (existingUser != null)
        {
            _logger.LogWarning("Register failed. Email already exists: {Email}", email);
            return "Bu email zaten kayıtlı";
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            FullName = dto.FullName,
            Email = email,
            PasswordHash = passwordHash
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        _logger.LogInformation("User registered successfully. UserId: {UserId}, Email: {Email}", user.Id, email);

        return "Kayıt başarılı";
    }

    // LOGIN
    public async Task<string?> LoginAsync(LoginDto dto)
    {
        var email = dto.Email.Trim().ToLower();

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
        {
            _logger.LogWarning("Login failed. User not found. Email: {Email}", email);
            return null;
        }

        var isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

        if (!isPasswordValid)
        {
            _logger.LogWarning("Login failed. Invalid password. UserId: {UserId}, Email: {Email}", user.Id, email);
            return null;
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("this_is_a_very_long_secret_key_for_fintrack_jwt_auth_2026"));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        _logger.LogInformation("User logged in successfully. UserId: {UserId}, Email: {Email}", user.Id, email);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}