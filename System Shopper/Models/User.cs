using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace System_Shopper.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(150)]
        public string UserProfileURL { get; set; } = string.Empty;

        public DateTime DateJoined { get; set; }

        [StringLength(200)]
        public string PasswordHash { get; set; } = string.Empty;

        [StringLength(15)]
        public string Salt { get; set; } = string.Empty;

        public DateTime LastLoginTime { get; set; }

        public int CartId { get; set; }
    }
}
