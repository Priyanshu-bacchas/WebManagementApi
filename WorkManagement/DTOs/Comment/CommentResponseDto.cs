namespace WorkManagement.DTOs.Comment
{
    public class CommentResponseDto
    {
        public int Id { get; set; }

        public int WorkItemId { get; set; }

        public int UserId { get; set; }

        public string CommentText { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}