using WorkManagement.DTOs.Auth;

namespace WorkManagement.Services.Interfaces;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterDto dto);

    Task<string?> LoginAsync(LoginDto dto);
}