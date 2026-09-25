using System.ComponentModel.DataAnnotations;

namespace WorkManagement.DTOs.Comment
{
    public class CommentUpdateDto
    {
        [Required]
        public string CommentText { get; set; } = null!;
    }
}