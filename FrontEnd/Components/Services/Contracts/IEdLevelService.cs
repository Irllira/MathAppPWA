using DTO.DTOs;

namespace FrontEnd.Components.Services.Contracts
{
    public interface IEdLevelService
    {
        Task<IEnumerable<EducationLevelDTO>> GetEducationLevels();
    }
}
