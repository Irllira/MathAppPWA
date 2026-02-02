using API.Enteties;

namespace API.Interfaces
{
    public interface IUserProgressRepo
    {
        Task<IEnumerable<UserProgress>>? GetUserProgressByUserId(int userID);

        Task<UserProgress>? GetUserProgressByUserIdUnitIdType(int userId, int unitId, string type);
        Task AddProgress(UserProgress userProgress);
        Task<IEnumerable<UserProgress>>? GetUserProgress();
        Task UpdateProgress(int id, string type, int all, int good);
    }
}
