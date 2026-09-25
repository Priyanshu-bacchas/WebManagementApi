using WorkManagement.Models;

namespace WorkManagement.Repositories.Interfaces;

public interface IWorkItemRepository
{
    Task<WorkItem?> GetByIdAsync(int id);
    Task<List<WorkItem>> GetAllAsync();
    Task<List<WorkItem>> GetByProjectAsync(int projectId);
    Task<List<WorkItem>> GetByAssigneeAsync(int userId);
    Task<WorkItem> AddAsync(WorkItem workItem);
    Task<WorkItem> UpdateAsync(WorkItem workItem);
    Task<bool> DeleteAsync(int id);
}