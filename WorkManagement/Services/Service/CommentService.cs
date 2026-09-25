using WorkManagement.DTOs.Comment;
using WorkManagement.Models;
using WorkManagement.Repositories.Interfaces;
using WorkManagement.Services.Interfaces;

namespace WorkManagement.Services.Implementations;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _repository;

    public CommentService(ICommentRepository repository)
    {
        _repository = repository;
    }

    public async Task<CommentResponseDto?> GetByIdAsync(
        int id)
    {
        var comment =
            await _repository.GetByIdAsync(id);

        if (comment == null)
            return null;

        return MapToDto(comment);
    }

    public async Task<List<CommentResponseDto>>
        GetByWorkItemAsync(int workItemId)
    {
        var comments =
            await _repository.GetByWorkItemAsync(workItemId);

        return comments
            .Select(MapToDto)
            .ToList();
    }

    public async Task<CommentResponseDto> CreateAsync(
        CommentCreateDto dto)
    {
        var comment = new Comment
        {
            WorkItemId = dto.WorkItemId,
            UserId = dto.UserId,
            CommentText = dto.CommentText,
            CreatedAt = DateTime.Now
        };

        var createdComment =
            await _repository.AddAsync(comment);

        return MapToDto(createdComment);
    }

    public async Task<CommentResponseDto?> UpdateAsync(
        int id,
        CommentUpdateDto dto)
    {
        var existingComment =
            await _repository.GetByIdAsync(id);

        if (existingComment == null)
            return null;

        existingComment.CommentText = dto.CommentText;
        existingComment.UpdatedAt = DateTime.Now;

        var updatedComment =
            await _repository.UpdateAsync(existingComment);

        return MapToDto(updatedComment);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static CommentResponseDto MapToDto(
        Comment comment)
    {
        return new CommentResponseDto
        {
            Id = comment.Id,
            WorkItemId = comment.WorkItemId,
            UserId = comment.UserId,
            CommentText = comment.CommentText,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt
        };
    }
}