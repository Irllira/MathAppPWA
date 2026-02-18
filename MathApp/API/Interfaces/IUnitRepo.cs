using MathApp.Backend.Data.Enteties;

namespace MathApp.Backend.API.Interfaces
{
    public interface IUnitRepo
    {
        Task<IEnumerable<Unit>> GetAllUnit();
        Task<Unit>? GetUnitByID(int ID);
        Task<Unit>? GetUnitByName(string Name);
        Task<IEnumerable<Unit>> GetUnitsbyEdLevel(int edLevID);
        Task<Unit> AddUnit(Unit unit);
        Task<Unit> AddUnit(string name, string? description, int EducationID, List<Definition>? definitions);
        //Task<IEnumerable<Unit>> AddUnits(IEnumerable<Unit> units);
        Task<bool> RemoveUnit(int ID);
        Task<bool> RemoveUnitByName(string name);
        Task<Unit> EditUnit(int Id, string name, string? description, int EducationID);
    }
}
