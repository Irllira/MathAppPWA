using DTO.DTOs;
using FrontEnd.Components.Pages.Admin.Unit;
using FrontEnd.Components.Services.Contracts;
using System.Net.Http.Json;

namespace FrontEnd.Components.Services
{
    public class DefinitionService : IDefinitionService
    {
        private readonly HttpClient _httpClient;

        public DefinitionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<DefinitionDTO>> GetDefinitions()
        {
            var response = await _httpClient.GetAsync($"/api/Definition");

            return await response.Content.ReadFromJsonAsync<IEnumerable<DefinitionDTO>>();
        }

        public async Task<IEnumerable<DefinitionDTO>> GetDefinitionsByUnit(string unitName)
        {
            var response = await _httpClient.GetAsync($"/api/Definition/DefinitionByUnit/" + unitName);

            return await response.Content.ReadFromJsonAsync<IEnumerable<DefinitionDTO>>();
        }

        public async Task<bool> AddDefinition(DefinitionDTO definition)
        {
            string s = "/api/Definition";
            var response = await _httpClient.PostAsJsonAsync(s, definition);

            if (response.IsSuccessStatusCode == true)
            {
                return true;
            }
            return false;
            //return await response.Content.ReadFromJsonAsync<IEnumerable<AccountsDTO>>();
        }

        public async Task DeleteDefinition(DefinitionDTO definition)
        {
            string s = "/api/Definition/Delete/" + definition.name;
            var response = await _httpClient.DeleteAsync(s);
        }

        public async Task<bool> UpdateDefinition(DefinitionDTO definition)
        {
            string s = "/api/Definition/Update";
            var response = await _httpClient.PostAsJsonAsync(s, definition);
            if (response.IsSuccessStatusCode == true)
            {
                return true;
            }
            return false;
        }
    }
}

