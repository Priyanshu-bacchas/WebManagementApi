using WorkManagement.DTOs.ProjectMember;

namespace WorkManagement.Services.Interfaces;

public interface IProjectMemberService
{
    Task<ProjectMemberResponseDto?> GetByIdAsync(int id);

    Task<List<ProjectMemberResponseDto>> GetByProjectAsync(int projectId);

    Task<List<ProjectMemberResponseDto>> GetByUserAsync(int userId);

    Task<ProjectMemberResponseDto> CreateAsync(ProjectMemberCreateDto dto);

    Task<ProjectMemberResponseDto?> UpdateAsync(
        int id,
        ProjectMemberUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}