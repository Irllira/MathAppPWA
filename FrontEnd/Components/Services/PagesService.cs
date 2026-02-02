using DTO.DTOs;
using FrontEnd.Components.Services.Contracts;

namespace FrontEnd.Components.Services
{
    public class PagesService:IPagesService
    {
        private readonly HttpClient _httpClient;

        public PagesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddPage(PagesDTO page)
        {
            string s = "/api/Pages";
            var response = await _httpClient.PostAsJsonAsync(s, page);

            if (response.IsSuccessStatusCode == true)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdatePage(PagesDTO page)
        {
            string s = "/api/Pages/UpdatePage";
            var response = await _httpClient.PostAsJsonAsync(s, page);

            if (response.IsSuccessStatusCode == true)
            {
                return true;
            }
            return false;
        }

        public async Task DeletePage(int pageID)
        {
            string s = "/api/Pages/DeletePage/" + pageID;
            var response = await _httpClient.DeleteAsync(s);
        }

        public async Task<IEnumerable<PagesDTO>> GetPagesByUnitID(int unitID)
        {
            string s = "/api/Pages/ByUnitID/" + unitID;
            var response = await _httpClient.GetAsync(s);
            return await response.Content.ReadFromJsonAsync<IEnumerable<PagesDTO>>();
        }
    }
}
