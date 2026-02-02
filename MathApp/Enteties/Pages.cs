using System.Reflection.Metadata;

namespace API.Enteties
{
    public class Pages
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public string? Description { get; set; }

        public required string link { get; set; }

        public required int UnitID { get; set; }
    }
}
