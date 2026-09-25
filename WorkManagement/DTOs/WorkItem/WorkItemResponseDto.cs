namespace WorkManagement.DTOs.WorkItem
{
    public class WorkItemResponseDto
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public int? AssignedTo { get; set; }

        public int CreatedBy { get; set; }

        public string Priority { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateOnly? DueDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}