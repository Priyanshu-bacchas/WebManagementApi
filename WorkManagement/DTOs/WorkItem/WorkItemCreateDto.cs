using System.ComponentModel.DataAnnotations;

namespace WorkManagement.DTOs.WorkItem
{
    public class WorkItemCreateDto
    {
        [Required]
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public int? AssignedTo { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [MaxLength(50)]
        public string Priority { get; set; } = "Medium";

        [MaxLength(50)]
        public string Status { get; set; } = "To Do";

        public DateOnly? DueDate { get; set; }
    }
}