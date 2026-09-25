using WorkManagement.Models;

namespace WorkManagement.Repositories.Interfaces;

public interface ICommentRepository
{
    Task<Comment?> GetByIdAsync(int id);
    Task<List<Comment>> GetByWorkItemAsync(int workItemId);
    Task<Comment> AddAsync(Comment comment);
    Task<Comment> UpdateAsync(Comment comment);
    Task<bool> DeleteAsync(int id);
}