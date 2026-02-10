using DTO.DTOs;
using FrontEnd.Components.Services.Contracts;

namespace FrontEnd.Components.Services
{
    public class EdLevelService: IEdLevelService
    {
        private readonly HttpClient _httpClient;

        public EdLevelService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<EducationLevelDTO>> GetEducationLevels()
        {
            var response = await _httpClient.GetAsync($"/api/EducationLevel");

            return await response.Content.ReadFromJsonAsync<IEnumerable<EducationLevelDTO>>();
        }
    }
}
