using System.ComponentModel.DataAnnotations;

namespace Dewalt.Models
{
    public class Member
    {
        [Required]
        public Guid MemberId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public char Gender { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
