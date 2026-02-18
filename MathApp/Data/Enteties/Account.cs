
namespace MathApp.Backend.Data.Enteties
{
    public class Account
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Salt { get; set; }
        public required bool isActive { get; set; }
        public required string Role { get; set; }

        public virtual List<UserProgress>? UserProgresses { get; set; }

    }
}
