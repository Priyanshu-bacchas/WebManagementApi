
using WorkManagement.DTOs.ProjectMember;
using WorkManagement.Models;
using WorkManagement.Repositories.Interfaces;
using WorkManagement.Services.Interfaces;

namespace WorkManagement.Services.Implementations;

public class ProjectMemberService : IProjectMemberService
{
    private readonly IProjectMemberRepository _repository;

    public ProjectMemberService(
        IProjectMemberRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProjectMemberResponseDto?> GetByIdAsync(
        int id)
    {
        var member =
            await _repository.GetByIdAsync(id);

        if (member == null)
            return null;

        return MapToDto(member);
    }

    public async Task<List<ProjectMemberResponseDto>>
        GetByProjectAsync(int projectId)
    {
        var members =
            await _repository.GetByProjectAsync(projectId);

        return members
            .Select(MapToDto)
            .ToList();
    }

    public async Task<List<ProjectMemberResponseDto>>
        GetByUserAsync(int userId)
    {
        var members =
            await _repository.GetByUserAsync(userId);

        return members
            .Select(MapToDto)
            .ToList();
    }

    public async Task<ProjectMemberResponseDto> CreateAsync(
        ProjectMemberCreateDto dto)
    {
        var member = new ProjectMember
        {
            ProjectId = dto.ProjectId,
            UserId = dto.UserId,
            Role = dto.Role,
            JoinedAt = DateTime.Now
        };

        var createdMember =
            await _repository.AddAsync(member);

        return MapToDto(createdMember);
    }

    public async Task<ProjectMemberResponseDto?> UpdateAsync(
        int id,
        ProjectMemberUpdateDto dto)
    {
        var existingMember =
            await _repository.GetByIdAsync(id);

        if (existingMember == null)
            return null;

        existingMember.ProjectId = dto.ProjectId;
        existingMember.UserId = dto.UserId;
        existingMember.Role = dto.Role;

        var updatedMember =
            await _repository.UpdateAsync(existingMember);

        return MapToDto(updatedMember);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static ProjectMemberResponseDto MapToDto(
        ProjectMember member)
    {
        return new ProjectMemberResponseDto
        {
            Id = member.Id,
            ProjectId = member.ProjectId,
            UserId = member.UserId,
            Role = member.Role,
            JoinedAt = member.JoinedAt
        };
    }
}

