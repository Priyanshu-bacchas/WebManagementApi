using Microsoft.EntityFrameworkCore;
using WorkManagement.Models;
using WorkManagement.Repositories.Interfaces;

namespace WorkManagement.Repositories.Implementations;

public class ProjectRepository : IProjectRepository
{
    private readonly WorkManagementDbContext _context;

    public ProjectRepository(WorkManagementDbContext context)
    {
        _context = context;
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await _context.Projects
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Project>> GetAllAsync()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task<List<Project>> GetByUserAsync(int userId)
    {
        return await _context.Projects
            .Where(x => x.CreatedBy == userId)
            .ToListAsync();
    }

    public async Task<Project> AddAsync(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return project;
    }

    public async Task<Project> UpdateAsync(Project project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();

        return project;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var project = await GetByIdAsync(id);

        if (project == null)
            return false;

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return true;
    }
}