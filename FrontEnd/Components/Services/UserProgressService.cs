using DTO.DTOs;
using FrontEnd.Components.Services.Contracts;
using System;

namespace FrontEnd.Components.Services
{
    public class UserProgressService: IUserProgressService
    {
        private readonly HttpClient _httpClient;

        public UserProgressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<UserProgressDTO>>? GetProgressByUser(string username)
        {
            var s = "/api/UserProgress/GetProgressByUser/"+username;
            var response = await _httpClient.GetAsync(s);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<UserProgressDTO>>();
            }else
            {
                return null;
            }
        }

        public async Task<UserProgressDTO>? GetProgressByAll (string username, string unitName, string type)
        {
            var s = "/api/UserProgress/GetProgressByUserUnitType/" + username + "/" + unitName + "/" + type;
            var response = await _httpClient.GetAsync(s);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserProgressDTO>();
            }else
            {
                return null;
            }
        }

        public async Task<bool> AddProgress(UserProgressDTO progress)
        {
            string s = "/api/UserProgress/NewProgress";
            var response = await _httpClient.PostAsJsonAsync(s, progress);

            if (response.IsSuccessStatusCode == true)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> AddProgressByName(string username, string unitName, string type, int good, int all)
        {
            string s = "/api/UserProgress/NewProgressByName/"+username+"/"+unitName + "/" + good + "/" + all + "/" + type;
            var response = await _httpClient.PostAsJsonAsync(s,"");

            if (response.IsSuccessStatusCode == true)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateProgress(UserProgressDTO progress)
        {
            string s = "/api/UserProgress/UpdateProgress";
            var response = await _httpClient.PostAsJsonAsync(s, progress);

            if (response.IsSuccessStatusCode == true)
            {
                return true;
            }
            return false;
        }
    }
}
