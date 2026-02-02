using API.Enteties;
using MathApp.Enteties;

namespace API.Interfaces
{
    public interface IPagesRepo
    {
        Task<IEnumerable<Pages>> GetAllPages();
        Task<IEnumerable<Pages>> GetPagesByUnit(int unitID);

        Task AddPage(Pages page);
        Task UpdatePage(int id, string name, string link, int unitId, string? description);

        Task DeletePage(int id);

    }
}
