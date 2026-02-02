using MathApp.Enteties;

namespace MathEducationWebApp.Components.Interfaces
{
    public interface IDefinitionRepo
    {
        Task<IEnumerable<Definition>> GetAllDefinitions();
        Definition? GetDefinitionbyId(int Id);
        Task<Definition> GetDefinitionbyName (string Name);
        Task<IEnumerable<Definition>> GetDefinitionsByUnit(int unitID);
        Task<IEnumerable<Definition>> GetDefinitionsByUnit(Unit unit);
        Task AddDefinition(Definition definition);
        Task AddDefinition(string name, string type, string part1, string part2, int unitId);

        Task AddDefinitions(IEnumerable<Definition> definitions);
        Task RemoveDefinition (Definition definition);
        Task RemoveDefinition(int Id);
        Task RemoveDefinition(string name);

        Task EditDefinition(int Id, string name, string type, string p1, string p2, int unitID);
    }
}
