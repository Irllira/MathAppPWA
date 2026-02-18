using MathApp.Backend.Data.Enteties;

namespace MathApp.Backend.API.Interfaces
{
    public interface IUserProgressRepo
    {
        Task<IEnumerable<UserProgress>>? GetUserProgressByUserId(int userID);

        Task<UserProgress>? GetUserProgressByUserIdUnitIdType(int userId, int unitId, string type);
        Task<UserProgress> AddProgress(UserProgress userProgress);
        Task<IEnumerable<UserProgress>>? GetUserProgress();
        Task<UserProgress> UpdateProgress(int id, string type, int all, int good);
    }
}
