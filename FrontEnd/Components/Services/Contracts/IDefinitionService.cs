using DTO.DTOs;

namespace FrontEnd.Components.Services.Contracts
{
    public interface IDefinitionService
    {
        Task<IEnumerable<DefinitionDTO>> GetDefinitions();
        Task<IEnumerable<DefinitionDTO>> GetDefinitionsByUnit(string name);

        Task<bool> AddDefinition(DefinitionDTO definition);

        Task DeleteDefinition(DefinitionDTO definition);

        Task<bool> UpdateDefinition(DefinitionDTO definition);
    }
}
