namespace WorkManagement.DTOs.Project
{
    public class ProjectResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int CreatedBy { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}