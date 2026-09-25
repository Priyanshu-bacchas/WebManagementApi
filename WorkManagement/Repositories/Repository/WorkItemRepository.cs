using Microsoft.EntityFrameworkCore;
using WorkManagement.Models;
using WorkManagement.Repositories.Interfaces;

namespace WorkManagement.Repositories.Implementations;

public class WorkItemRepository : IWorkItemRepository
{
    private readonly WorkManagementDbContext _context;

    public WorkItemRepository(WorkManagementDbContext context)
    {
        _context = context;
    }

    public async Task<WorkItem?> GetByIdAsync(int id)
    {
        return await _context.WorkItems
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<WorkItem>> GetAllAsync()
    {
        return await _context.WorkItems.ToListAsync();
    }

    public async Task<List<WorkItem>> GetByProjectAsync(int projectId)
    {
        return await _context.WorkItems
            .Where(x => x.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task<List<WorkItem>> GetByAssigneeAsync(int userId)
    {
        return await _context.WorkItems
            .Where(x => x.AssignedTo == userId)
            .ToListAsync();
    }

    public async Task<WorkItem> AddAsync(WorkItem workItem)
    {
        _context.WorkItems.Add(workItem);
        await _context.SaveChangesAsync();

        return workItem;
    }

    public async Task<WorkItem> UpdateAsync(WorkItem workItem)
    {
        _context.WorkItems.Update(workItem);
        await _context.SaveChangesAsync();

        return workItem;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var workItem = await GetByIdAsync(id);

        if (workItem == null)
            return false;

        _context.WorkItems.Remove(workItem);
        await _context.SaveChangesAsync();

        return true;
    }
}