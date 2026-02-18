using MathApp.Backend.Data.Enteties;

namespace MathApp.Backend.API.Interfaces
{
    public interface IDefinitionRepo
    {
        Task<IEnumerable<Definition>> GetAllDefinitions();
        Task<Definition> GetDefinitionbyId(int Id);
        Task<Definition> GetDefinitionbyName (string Name);
        Task<IEnumerable<Definition>> GetDefinitionsByUnit(int unitID);
        Task<IEnumerable<Definition>> GetDefinitionsByUnit(Unit unit);
        Task<Definition> AddDefinition(Definition definition);
        Task<Definition> AddDefinition(string name, string type, string part1, string part2, int unitId);
        Task<bool> RemoveDefinition (Definition definition);
        Task<bool> RemoveDefinition(int Id);
        Task<bool> EditDefinition(int Id, string name, string type, string p1, string p2, int unitID);
    }
}
