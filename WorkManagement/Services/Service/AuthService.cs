using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WorkManagement.DTOs.Auth;
using WorkManagement.Models;
using WorkManagement.Repositories.Interfaces;
using WorkManagement.Services.Interfaces;

namespace WorkManagement.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserRepository _repository;
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _configuration;

    public AuthService(
        IUserRepository repository,
        IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<bool> RegisterAsync(RegisterDto dto)
    {
        var existingUser =
            await _repository.GetByEmailAsync(dto.Email);

        if (existingUser != null)
            return false;

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Role = "Member",
            CreatedAt = DateTime.Now
        };

        user.PasswordHash =
            _passwordHasher.HashPassword(
                user,
                dto.Password);

        await _repository.AddAsync(user);

        return true;
    }

    public async Task<string?> LoginAsync(LoginDto dto)
    {
        var user =
            await _repository.GetByEmailAsync(dto.Email);

        if (user == null)
            return null;

        var result =
            _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                dto.Password);

        if (result == PasswordVerificationResult.Failed)
            return null;

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSection =
            _configuration.GetSection("Jwt");

        var key = jwtSection["Key"]!;
        var issuer = jwtSection["Issuer"]!;
        var audience = jwtSection["Audience"]!;

        var expiryMinutes =
            Convert.ToDouble(
                jwtSection["ExpiryMinutes"]);

        var claims = new List<Claim>
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()),

            new Claim(
                ClaimTypes.Name,
                user.Name),

            new Claim(
                ClaimTypes.Email,
                user.Email),

            new Claim(
                ClaimTypes.Role,
                user.Role)
        };

        var securityKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key));

        var credentials =
            new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                expiryMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}