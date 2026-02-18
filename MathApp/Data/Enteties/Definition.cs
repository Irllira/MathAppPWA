
namespace MathApp.Backend.Data.Enteties
{
    public class Definition
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Type { get; set; }
        public required string Part1 { get; set; }
        public required string Part2 { get; set; }
        public required int unitId { get; set; }
        public virtual List<DefIncPair>? defIncPairs { get; set; }

    }
}
