using System.ComponentModel.DataAnnotations;

namespace WorkManagement.DTOs.Comment
{
    public class CommentCreateDto
    {
        [Required]
        public int WorkItemId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string CommentText { get; set; } = null!;
    }
}