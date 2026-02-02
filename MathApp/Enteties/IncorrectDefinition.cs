namespace API.Enteties
{
    public class IncorrectDefinition
    {
        public int Id { get; set; }
        public required string content { get; set; }

        public virtual List<DefIncPair>? defIncPairs { get; set; }
    }
}
