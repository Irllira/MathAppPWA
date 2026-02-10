using DTO.DTOs;
using FrontEnd.Components.Services.Contracts;

namespace FrontEnd.Components.Services
{
    public class UnitService : IUnitService
    {
        private readonly HttpClient _httpClient;

        public UnitService(HttpClient httpClient) 
        { 
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<UnitDTO>> GetUnits()
        {
            var response = await _httpClient.GetAsync($"/api/Unit");

            return await response.Content.ReadFromJsonAsync<IEnumerable<UnitDTO>>();
        }

        public async Task<IEnumerable<UnitDTO>> GetUnitsByEdLvlName(string edLvlName)
        {
            var response = await _httpClient.GetAsync($"/api/Unit/ByEdLvl/"+edLvlName);
       
            return await response.Content.ReadFromJsonAsync<IEnumerable<UnitDTO>>();
        }

        public async Task<UnitDTO> GetUnitByName(string name)
        {
            var response = await _httpClient.GetAsync($"/api/Unit/ByName/" + name);

            return await response.Content.ReadFromJsonAsync<UnitDTO>();
        }

        public async Task<bool> AddUnit(UnitDTO unit)
        {
            string s = "/api/Unit";
            var response = await _httpClient.PostAsJsonAsync(s, unit);

            if (response.IsSuccessStatusCode == true)
            {
                return true;
            }
            return false;
            
        }

        public async Task DeleteUnit(UnitDTO unit)
        {
            string s = "/api/Unit/Delete/"+unit.name;
            var response = await _httpClient.DeleteAsync(s);

        }

        public async Task<bool> UpdateUnit(UnitDTO unit)
        {
            string s = "/api/Unit/Update";
            var response = await _httpClient.PostAsJsonAsync(s, unit);

            if (response.IsSuccessStatusCode == true)
            {
                return true;
            }
            return false;
        }
    }
}
