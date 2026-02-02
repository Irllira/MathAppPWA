using MathApp.Enteties;

namespace MathEducationWebApp.Components.Interfaces
{
    public interface IEducationLevelRepo
    {
        Task<IEnumerable<EducationLevel>> GetAllEducationLevels();
        Task<EducationLevel>? GetEducationLevelByID(int IDNumber);
        Task<EducationLevel>? GetEducationLevelsbyName(string name);
        Task<string> GetEducationLevelName(int IDNumber);
    }

}
