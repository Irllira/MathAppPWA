using API.Enteties;
using DTO.DTOs;

namespace FrontEnd.Components.Services.Contracts
{
    public interface IIncorrectService
    {
        Task<IEnumerable<IncorrectDTO>> GetAllIncorrect();

        Task<IEnumerable<IncorrectDTO>> GetIncorrectByDefs(int defID);
        Task<IncorrectDTO> GetIncorrectByContent(string content);

        Task<IEnumerable<DefIncPairDTO>> GetAllPairs();
        Task<IEnumerable<DefIncPairDTO>> GetPairsByDef(int defID);

        Task<IncorrectDTO> AddIncorrect(IncorrectDTO incorrect);

        Task AddPair(int incorrectId, int definitionId);
        //Task Delete(IncorrectDTO incorrect);
        Task DeletePair(int defID, int incID);
    }
}
