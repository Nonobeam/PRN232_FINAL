using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO
{
    public class RegisterRequest
    {
        public int RoleId { get; set; } = 4; // Default to Customer role for public registration

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty;

        public DateOnly? Dob { get; set; }

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
