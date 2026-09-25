using WorkManagement.DTOs.WorkItem;

namespace WorkManagement.Services.Interfaces;

public interface IWorkItemService
{
    Task<WorkItemResponseDto?> GetByIdAsync(int id);

    Task<List<WorkItemResponseDto>> GetAllAsync();

    Task<List<WorkItemResponseDto>> GetByProjectAsync(int projectId);

    Task<List<WorkItemResponseDto>> GetByAssigneeAsync(int userId);

    Task<WorkItemResponseDto> CreateAsync(WorkItemCreateDto dto);

    Task<WorkItemResponseDto?> UpdateAsync(
        int id,
        WorkItemUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}