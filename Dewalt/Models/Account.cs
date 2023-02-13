namespace Dewalt.Models
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public char Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
