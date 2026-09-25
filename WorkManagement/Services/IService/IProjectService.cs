using WorkManagement.DTOs.Project;

namespace WorkManagement.Services.Interfaces;

public interface IProjectService
{
    Task<ProjectResponseDto?> GetByIdAsync(int id);

    Task<List<ProjectResponseDto>> GetAllAsync();

    Task<List<ProjectResponseDto>> GetByUserAsync(int userId);

    Task<ProjectResponseDto> CreateAsync(ProjectCreateDto dto);

    Task<ProjectResponseDto?> UpdateAsync(int id, ProjectUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}