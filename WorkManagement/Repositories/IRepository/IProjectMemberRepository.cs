using WorkManagement.Models;


namespace WorkManagement.Repositories.Interfaces;

public interface IProjectMemberRepository
{
    Task<ProjectMember?> GetByIdAsync(int id);
    Task<List<ProjectMember>> GetByProjectAsync(int projectId);
    Task<List<ProjectMember>> GetByUserAsync(int userId);
    Task<ProjectMember> AddAsync(ProjectMember member);
    Task<ProjectMember> UpdateAsync(ProjectMember member);
    Task<bool> DeleteAsync(int id);
}