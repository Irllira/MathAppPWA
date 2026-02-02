using DTO.DTOs;

namespace FrontEnd.Components.Services.Contracts
{
    public interface IPagesService
    {
        Task<IEnumerable<PagesDTO>> GetPagesByUnitID(int unitID);

        Task DeletePage(int pageID);
        Task<bool> UpdatePage(PagesDTO page);
        Task<bool> AddPage(PagesDTO page);
    }
}
