using System.ComponentModel.DataAnnotations;

namespace Influence_Hair_Care.Models.Dtos
{
    public class LoginDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long LoginId { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
