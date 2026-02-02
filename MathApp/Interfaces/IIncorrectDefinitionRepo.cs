using API.Enteties;
using API.Repos;
using MathApp.Enteties;

namespace API.Interfaces
{
    public interface IIncorrectDefinitionRepo
    {
        Task<IEnumerable<IncorrectDefinition>> GetAllIncorrect();
        Task<IncorrectDefinition> GetIncorrectByID(int id);
        Task<IncorrectDefinition> GetIncorrectByContent(string s);

        Task<IEnumerable<IncorrectDefinition>> GetIncorrectsByDefinition(int definitionId);

        Task AddIncorrect(string content);
        Task AddIncorrect(IncorrectDefinition def);
        Task AddPair(int defId, int incId);
        Task DeletePair(int defId, int incId);
        Task<IEnumerable<DefIncPair>> GetAllPairs();
        Task<IEnumerable<DefIncPair>> GetPairsByDefinition(int definitionID);

    }
}
