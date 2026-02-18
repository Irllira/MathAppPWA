using MathApp.Backend.Data.Enteties;

namespace MathApp.Backend.API.Interfaces
{
    public interface IIncorrectDefinitionRepo
    {
        Task<IEnumerable<IncorrectDefinition>> GetAllIncorrect();
        Task<IncorrectDefinition> GetIncorrectByID(int id);
        Task<IncorrectDefinition> GetIncorrectByContent(string s);

        Task<IEnumerable<IncorrectDefinition>> GetIncorrectsByDefinition(int definitionId);

        Task<IncorrectDefinition> AddIncorrect(string content);
        Task<IncorrectDefinition> AddIncorrect(IncorrectDefinition def);
        Task<DefIncPair> AddPair(int defId, int incId);
        Task<bool> DeletePair(int defId, int incId);
        Task<IEnumerable<DefIncPair>> GetAllPairs();
        Task<IEnumerable<DefIncPair>> GetPairsByDefinition(int definitionID);

    }
}
