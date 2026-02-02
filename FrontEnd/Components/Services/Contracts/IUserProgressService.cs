using DTO.DTOs;

namespace FrontEnd.Components.Services.Contracts
{
    public interface IUserProgressService
    {
        Task<IEnumerable<UserProgressDTO>>? GetProgressByUser(string username);

        Task<UserProgressDTO>? GetProgressByAll(string username, string unitName, string type);
        Task<bool> AddProgress(UserProgressDTO progress);
        Task<bool> AddProgressByName(string username, string unitName, string type, int good, int all);

        Task<bool> UpdateProgress(UserProgressDTO progress);
    }
}
