using MathApp.Backend.Data.Enteties;


namespace MathApp.Backend.API.Interfaces
{
    public interface IPagesRepo
    {
        Task<IEnumerable<Pages>> GetAllPages();
        Task<IEnumerable<Pages>> GetPagesByUnit(int unitID);

        Task<Pages> AddPage(Pages page);
        Task<Pages> UpdatePage(int id, string name, string link, int unitId);

        Task<bool> DeletePage(int id);

    }
}
