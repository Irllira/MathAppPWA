namespace MathApp.Backend.Data.Enteties
{
    public class EducationLevel
    {
        public int Id { get; set; }
        public required string name { get; set; }
        public virtual List<Unit>? Units { get; set; }

    }
}
