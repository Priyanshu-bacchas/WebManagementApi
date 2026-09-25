using System.ComponentModel.DataAnnotations;

namespace WorkManagement.DTOs.User
{
    public class UserCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [MaxLength(50)]
        public string Role { get; set; } = "Member";
    }
}