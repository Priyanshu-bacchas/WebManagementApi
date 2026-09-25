using Microsoft.AspNetCore.Identity;
using WorkManagement.DTOs.User;
using WorkManagement.Models;
using WorkManagement.Repositories.Interfaces;
using WorkManagement.Services.Interfaces;

namespace WorkManagement.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly PasswordHasher<User> _passwordHasher;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<UserResponseDto?> GetByIdAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user == null)
            return null;

        return MapToDto(user);
    }

    public async Task<List<UserResponseDto>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();

        return users
            .Select(MapToDto)
            .ToList();
    }

    public async Task<UserResponseDto> CreateAsync(
        UserCreateDto dto)
    {
        // Check duplicate email
        var existingUser =
            await _repository.GetByEmailAsync(dto.Email);

        if (existingUser != null)
            throw new Exception("Email already exists.");

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Role = dto.Role,
            CreatedAt = DateTime.Now
        };

        // IMPORTANT:
        // Plain password ko database me save nahi karna.
        // Password ko hash karke PasswordHash me save karenge.
        user.PasswordHash =
            _passwordHasher.HashPassword(
                user,
                dto.Password);

        var createdUser =
            await _repository.AddAsync(user);

        return MapToDto(createdUser);
    }

    public async Task<UserResponseDto?> UpdateAsync(
        int id,
        UserUpdateDto dto)
    {
        var existingUser =
            await _repository.GetByIdAsync(id);

        if (existingUser == null)
            return null;

        existingUser.Name = dto.Name;
        existingUser.Email = dto.Email;
        existingUser.Role = dto.Role;

        var updatedUser =
            await _repository.UpdateAsync(existingUser);

        return MapToDto(updatedUser);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static UserResponseDto MapToDto(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt
        };
    }
}