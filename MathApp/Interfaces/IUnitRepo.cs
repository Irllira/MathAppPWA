using MathApp.Enteties;

namespace MathEducationWebApp.Components.Interfaces
{
    public interface IUnitRepo
    {
        Task<IEnumerable<Unit>> GetAllUnit();
        Task<Unit>? GetUnitByID(int ID);
        Task<Unit>? GetUnitByName(string Name);
        Task<IEnumerable<Unit>> GetUnitsbyEdLevel(int edLevID);
        Task AddUnit(Unit unit);
        Task AddUnit(string name, string? description, int EducationID, List<Definition>? definitions);
        Task AddUnits(IEnumerable<Unit> units);
        Task RemoveUnit(int ID);
        Task RemoveUnitByName(string name);

        Task EditUnit(int Id, string name, string? description, int EducationID);
    }
}
