
namespace MathApp.Backend.Data.Enteties
{
    public class Pages
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        //public string? Description { get; set; }

        public required string Link { get; set; }

        public required int UnitID { get; set; }
    }
}
