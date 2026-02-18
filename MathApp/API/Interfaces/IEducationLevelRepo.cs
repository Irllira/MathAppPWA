using MathApp.Backend.Data.Enteties;

namespace MathApp.Backend.API.Interfaces
{
    public interface IEducationLevelRepo
    {
        Task<IEnumerable<EducationLevel>> GetAllEducationLevels();
        Task<EducationLevel>? GetEducationLevelByID(int IDNumber);
        Task<EducationLevel>? GetEducationLevelsbyName(string name);
        Task<string> GetEducationLevelName(int IDNumber);
    }

}
