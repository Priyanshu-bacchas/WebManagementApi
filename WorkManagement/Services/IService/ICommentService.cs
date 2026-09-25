using WorkManagement.DTOs.Comment;

namespace WorkManagement.Services.Interfaces;

public interface ICommentService
{
    Task<CommentResponseDto?> GetByIdAsync(int id);

    Task<List<CommentResponseDto>> GetByWorkItemAsync(int workItemId);

    Task<CommentResponseDto> CreateAsync(CommentCreateDto dto);

    Task<CommentResponseDto?> UpdateAsync(
        int id,
        CommentUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}