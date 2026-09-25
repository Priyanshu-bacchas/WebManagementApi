using WorkManagement.DTOs.Project;
using WorkManagement.Models;
using WorkManagement.Repositories.Interfaces;
using WorkManagement.Services.Interfaces;

namespace WorkManagement.Services.Implementations;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _repository;

    public ProjectService(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProjectResponseDto?> GetByIdAsync(int id)
    {
        var project =
            await _repository.GetByIdAsync(id);

        if (project == null)
            return null;

        return MapToDto(project);
    }

    public async Task<List<ProjectResponseDto>> GetAllAsync()
    {
        var projects =
            await _repository.GetAllAsync();

        return projects
            .Select(MapToDto)
            .ToList();
    }

    public async Task<List<ProjectResponseDto>> GetByUserAsync(
        int userId)
    {
        var projects =
            await _repository.GetByUserAsync(userId);

        return projects
            .Select(MapToDto)
            .ToList();
    }

    public async Task<ProjectResponseDto> CreateAsync(
        ProjectCreateDto dto)
    {
        var project = new Project
        {
            Name = dto.Name,
            Description = dto.Description,
            CreatedBy = dto.CreatedBy,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            CreatedAt = DateTime.Now
        };

        var createdProject =
            await _repository.AddAsync(project);

        return MapToDto(createdProject);
    }

    public async Task<ProjectResponseDto?> UpdateAsync(
        int id,
        ProjectUpdateDto dto)
    {
        var existingProject =
            await _repository.GetByIdAsync(id);

        if (existingProject == null)
            return null;

        existingProject.Name = dto.Name;
        existingProject.Description = dto.Description;
        existingProject.StartDate = dto.StartDate;
        existingProject.EndDate = dto.EndDate;

        var updatedProject =
            await _repository.UpdateAsync(existingProject);

        return MapToDto(updatedProject);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static ProjectResponseDto MapToDto(
        Project project)
    {
        return new ProjectResponseDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreatedBy = project.CreatedBy,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            CreatedAt = project.CreatedAt
        };
    }
}