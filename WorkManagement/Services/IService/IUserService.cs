using WorkManagement.DTOs.User;

namespace WorkManagement.Services.Interfaces;

public interface IUserService
{
    Task<UserResponseDto?> GetByIdAsync(int id);

    Task<List<UserResponseDto>> GetAllAsync();

    Task<UserResponseDto> CreateAsync(UserCreateDto dto);

    Task<UserResponseDto?> UpdateAsync(int id, UserUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}