using API.Enteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathApp.Enteties
{
    public class Definition
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public string? Type { get; set; }
        public required string Part1 { get; set; }
        public required string Part2 { get; set; }
        public required int unitId { get; set; }

        //public virtual List<IncorrectDefinition>? IncorrectDefinitions { get; set; }

          public virtual List<DefIncPair>? defIncPairs { get; set; }
      /*  public void Configure(EntityTypeBuilder<Definition> builder)
        {
            builder.HasMany(x => x.Campaigns)
                    .WithOne(x => x.State).OnDelete(DeleteBehavior.ClientSetNull);
        }*/

    }
}
