
using System.ComponentModel.DataAnnotations;

namespace WorkManagement.DTOs.ProjectMember
{
    public class ProjectMemberUpdateDto
    {
        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = "Member";
    }
}

