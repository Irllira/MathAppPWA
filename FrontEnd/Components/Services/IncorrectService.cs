using API.Enteties;
using DTO.DTOs;
using FrontEnd.Components.Services.Contracts;
using System.Net.Http.Json;

namespace FrontEnd.Components.Services
{
    public class IncorrectService : IIncorrectService
    {
        private readonly HttpClient _httpClient;

        public IncorrectService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddPair(int incorrectId, int definitionId)
        {
            string s = "/api/Incorrect/AddPair";
            var pair = new DefIncPairDTO() { IncorrectID=incorrectId, DefinitionID=definitionId };
            var response = await _httpClient.PostAsJsonAsync(s, pair);
        }

        public async Task<IncorrectDTO> AddIncorrect(IncorrectDTO incorrect)
        {
            string s = "/api/Incorrect/NewIncorrect";
            var response = await _httpClient.PostAsJsonAsync(s, incorrect);

            return await response.Content.ReadFromJsonAsync<IncorrectDTO>();

        }

        public async Task<IEnumerable<IncorrectDTO>> GetAllIncorrect()
        {
            string s = "/api/Incorrect";
            var response = await _httpClient.GetAsync(s);

            return await response.Content.ReadFromJsonAsync<IEnumerable<IncorrectDTO>>();
        }

        public async Task<IEnumerable<IncorrectDTO>> GetIncorrectByDefs(int defID)
        {
            string s = "/api/Incorrect/ByDefinition/" + defID;
            var response = await _httpClient.GetAsync(s);

           return await response.Content.ReadFromJsonAsync<IEnumerable<IncorrectDTO>>();
        }

        public async Task<IncorrectDTO> GetIncorrectByContent(string content)
        {
         //   var c = System.Web.HttpUtility.UrlEncode(content);
            string s = "/api/Incorrect/ByContent/"+content;

            var response = await _httpClient.GetAsync(s);

            return await response.Content.ReadFromJsonAsync<IncorrectDTO>();

        }


        public async Task DeletePair(int defID, int incID)
        {
            string s = "/api/Incorrect/DeletePair/"+defID+"/"+incID;
            await _httpClient.DeleteAsync(s);
        }

        public async Task<IEnumerable<DefIncPairDTO>> GetAllPairs()
        {
            string s = "/api/Incorrect/Pairs";
            var response = await _httpClient.GetAsync(s);

            return await response.Content.ReadFromJsonAsync<IEnumerable<DefIncPairDTO>>();
        }

        public async Task<IEnumerable<DefIncPairDTO>> GetPairsByDef(int defID)
        {
            string s = "/api/Incorrect/PairsByDef/"+defID;
            var response = await _httpClient.GetAsync(s);

            return await response.Content.ReadFromJsonAsync<IEnumerable<DefIncPairDTO>>();
        }
    }
}
