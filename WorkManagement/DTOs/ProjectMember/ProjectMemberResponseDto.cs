namespace WorkManagement.DTOs.ProjectMember
{
    public class ProjectMemberResponseDto
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public int UserId { get; set; }

        public string Role { get; set; } = null!;

        public DateTime JoinedAt { get; set; }
    }
}