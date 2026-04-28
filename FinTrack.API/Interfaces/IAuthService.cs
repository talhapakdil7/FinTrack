using FinTrack.API.DTOs;

namespace FinTrack.API.Interfaces;

public interface IAuthService
{
    // Kullanıcı kayıt olur
    Task<string> RegisterAsync(RegisterDto dto);

    // Kullanıcı giriş yapar
    Task<string?> LoginAsync(LoginDto dto);
}

