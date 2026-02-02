using DTO.DTOs;

namespace FrontEnd.Components.Services.Contracts
{
    public interface IUnitService
    {
        Task<IEnumerable<UnitDTO>> GetUnits();
        Task<IEnumerable<UnitDTO>> GetUnitsByEdLvlName(string edLvlName);
        Task<UnitDTO> GetUnitByName(string name);
        Task<bool> AddUnit(UnitDTO unit);

        Task DeleteUnit(UnitDTO unit);

        Task<bool> UpdateUnit(UnitDTO unit);
    }
}
