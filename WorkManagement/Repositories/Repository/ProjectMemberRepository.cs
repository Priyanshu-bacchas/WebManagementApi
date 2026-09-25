using Microsoft.EntityFrameworkCore;
using WorkManagement.Models;
using WorkManagement.Repositories.Interfaces;

namespace WorkManagement.Repositories.Implementations;

public class ProjectMemberRepository : IProjectMemberRepository
{
    private readonly WorkManagementDbContext _context;

    public ProjectMemberRepository(WorkManagementDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectMember?> GetByIdAsync(int id)
    {
        return await _context.ProjectMembers
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<ProjectMember>> GetByProjectAsync(int projectId)
    {
        return await _context.ProjectMembers
            .Where(x => x.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task<List<ProjectMember>> GetByUserAsync(int userId)
    {
        return await _context.ProjectMembers
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task<ProjectMember> AddAsync(ProjectMember member)
    {
        _context.ProjectMembers.Add(member);
        await _context.SaveChangesAsync();

        return member;
    }

    public async Task<ProjectMember> UpdateAsync(ProjectMember member)
    {
        _context.ProjectMembers.Update(member);
        await _context.SaveChangesAsync();

        return member;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var member = await GetByIdAsync(id);

        if (member == null)
            return false;

        _context.ProjectMembers.Remove(member);
        await _context.SaveChangesAsync();

        return true;
    }
}