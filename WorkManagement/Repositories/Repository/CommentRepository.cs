using Microsoft.EntityFrameworkCore;
using WorkManagement.Models;
using WorkManagement.Repositories.Interfaces;

namespace WorkManagement.Repositories.Implementations;

public class CommentRepository : ICommentRepository
{
    private readonly WorkManagementDbContext _context;

    public CommentRepository(WorkManagementDbContext context)
    {
        _context = context;
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Comment>> GetByWorkItemAsync(int workItemId)
    {
        return await _context.Comments
            .Where(x => x.WorkItemId == workItemId)
            .ToListAsync();
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return comment;
    }

    public async Task<Comment> UpdateAsync(Comment comment)
    {
        _context.Comments.Update(comment);
        await _context.SaveChangesAsync();

        return comment;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var comment = await GetByIdAsync(id);

        if (comment == null)
            return false;

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        return true;
    }
}