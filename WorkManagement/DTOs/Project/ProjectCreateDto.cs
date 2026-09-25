using System.ComponentModel.DataAnnotations;

namespace WorkManagement.DTOs.Project
{
    public class ProjectCreateDto
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }
    }
}