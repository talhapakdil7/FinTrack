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

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    // REGISTER
    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        // Email daha önce var mı kontrol
         var email = dto.Email.Trim().ToLower();
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (existingUser != null)
            return "Bu email zaten kayıtlı";

        // Şifreyi hashliyoruz
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = passwordHash
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return "Kayıt başarılı";
    }

    // LOGIN
   public async Task<string?> LoginAsync(LoginDto dto)
{

    var email = dto.Email.Trim().ToLower();

    var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Email == dto.Email);

    if (user == null)
        return null;

    var isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

    if (!isPasswordValid)
        return null;

    //  TOKEN OLUŞTURMA
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this_is_a_very_long_secret_key_for_fintrack_jwt_auth_2026"));

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

    return new JwtSecurityTokenHandler().WriteToken(token);
}
}