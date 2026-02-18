
namespace MathApp.Backend.Data.Enteties
{
    public class Unit
    {
        public int Id { get; set; }
        public required string name { get; set; }
        public string? description { get; set; }
        public required int educationLevelId { get; set; }
        public virtual List<Definition>? definitions { get; set; }
        public virtual List<Pages>? pages { get; set; }
        public virtual List<UserProgress>? UserProgresses { get; set; }


    }
}
