using WorkManagement.Models;

namespace WorkManagement.Repositories.Interfaces;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(int id);
    Task<List<Project>> GetAllAsync();
    Task<List<Project>> GetByUserAsync(int userId);
    Task<Project> AddAsync(Project project);
    Task<Project> UpdateAsync(Project project);
    Task<bool> DeleteAsync(int id);
}