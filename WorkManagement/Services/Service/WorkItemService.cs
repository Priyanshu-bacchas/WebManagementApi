using WorkManagement.DTOs.WorkItem;
using WorkManagement.Models;
using WorkManagement.Repositories.Interfaces;
using WorkManagement.Services.Interfaces;

namespace WorkManagement.Services.Implementations;

public class WorkItemService : IWorkItemService
{
    private readonly IWorkItemRepository _repository;

    public WorkItemService(IWorkItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<WorkItemResponseDto?> GetByIdAsync(
        int id)
    {
        var workItem =
            await _repository.GetByIdAsync(id);

        if (workItem == null)
            return null;

        return MapToDto(workItem);
    }

    public async Task<List<WorkItemResponseDto>> GetAllAsync()
    {
        var workItems =
            await _repository.GetAllAsync();

        return workItems
            .Select(MapToDto)
            .ToList();
    }

    public async Task<List<WorkItemResponseDto>>
        GetByProjectAsync(int projectId)
    {
        var workItems =
            await _repository.GetByProjectAsync(projectId);

        return workItems
            .Select(MapToDto)
            .ToList();
    }

    public async Task<List<WorkItemResponseDto>>
        GetByAssigneeAsync(int userId)
    {
        var workItems =
            await _repository.GetByAssigneeAsync(userId);

        return workItems
            .Select(MapToDto)
            .ToList();
    }

    public async Task<WorkItemResponseDto> CreateAsync(
        WorkItemCreateDto dto)
    {
        var workItem = new WorkItem
        {
            ProjectId = dto.ProjectId,
            Title = dto.Title,
            Description = dto.Description,
            AssignedTo = dto.AssignedTo,
            CreatedBy = dto.CreatedBy,
            Priority = dto.Priority,
            Status = dto.Status,
            DueDate = dto.DueDate,
            CreatedAt = DateTime.Now
        };

        var createdWorkItem =
            await _repository.AddAsync(workItem);

        return MapToDto(createdWorkItem);
    }

    public async Task<WorkItemResponseDto?> UpdateAsync(
        int id,
        WorkItemUpdateDto dto)
    {
        var existingWorkItem =
            await _repository.GetByIdAsync(id);

        if (existingWorkItem == null)
            return null;

        existingWorkItem.Title = dto.Title;
        existingWorkItem.Description = dto.Description;
        existingWorkItem.AssignedTo = dto.AssignedTo;
        existingWorkItem.Priority = dto.Priority;
        existingWorkItem.Status = dto.Status;
        existingWorkItem.DueDate = dto.DueDate;
        existingWorkItem.UpdatedAt = DateTime.Now;

        var updatedWorkItem =
            await _repository.UpdateAsync(existingWorkItem);

        return MapToDto(updatedWorkItem);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static WorkItemResponseDto MapToDto(
        WorkItem workItem)
    {
        return new WorkItemResponseDto
        {
            Id = workItem.Id,
            ProjectId = workItem.ProjectId,
            Title = workItem.Title,
            Description = workItem.Description,
            AssignedTo = workItem.AssignedTo,
            CreatedBy = workItem.CreatedBy,
            Priority = workItem.Priority,
            Status = workItem.Status,
            DueDate = workItem.DueDate,
            CreatedAt = workItem.CreatedAt,
            UpdatedAt = workItem.UpdatedAt
        };
    }
}