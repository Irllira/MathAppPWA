namespace API.Enteties
{
    public class UserProgress
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public int AccountId { get; set; }
        public int good { get; set; }
        public int all { get; set; }

        public required string type { get; set; }
    }
}
