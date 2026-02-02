using DTO.DTOs;
using FrontEnd.Components.Services.Contracts;
using System.Net.Http.Json;

namespace FrontEnd.Components.Services
{
    public class AccountService : IAccountService
    {

        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddAccount(AccountsPasswordsDTO account)
        {
            string s = "/api/Accounts/AddAccount";
            var response = await _httpClient.PostAsJsonAsync(s,account);
            //_httpClient.
            if (response.IsSuccessStatusCode == true)
            {
                return true;
            }
            return false;
            //return await response.Content.ReadFromJsonAsync<IEnumerable<AccountsDTO>>();
        }

        public async Task<IEnumerable<AccountsDTO>> GetAccounts()
        {

            var response = await _httpClient.GetAsync($"/api/Accounts");
            // var x = response.Content.

            return await response.Content.ReadFromJsonAsync<IEnumerable<AccountsDTO>>();
        }

        public async Task<IEnumerable<AccountsPasswordsDTO>> GetAccountsPasswords()
        {

            var response = await _httpClient.GetAsync($"/api/Accounts/Login");
            // var x = response.Content.

            return await response.Content.ReadFromJsonAsync<IEnumerable<AccountsPasswordsDTO>>();
        }

        public async Task<AccountsPasswordsDTO>? GetAccountPasswordsByName(string name)
        {
            string s = "/api/Accounts/AccountByName/" + name;
            var response = await _httpClient.GetAsync(s);
            // var x = response.Content.
            if (response.IsSuccessStatusCode == true)
            {
                return await response.Content.ReadFromJsonAsync<AccountsPasswordsDTO>();
            }
            return null;           
        }

        public async Task<AccountsDTO>? GetAccountAdminByName(string name)
        {
            string s = "/api/Accounts/AccountAdminByName/" + name;
            var response = await _httpClient.GetAsync(s);
            // var x = response.Content.
            if (response.IsSuccessStatusCode == true)
            {
                return await response.Content.ReadFromJsonAsync<AccountsDTO>();
            }
            return null;
        }
        public async Task<AccountsDTO>? GetAccountAdminByEmail(string email)
        {
            string s = "/api/Accounts/AccountAdminByEmail/" + email;
            var response = await _httpClient.GetAsync(s);
            // var x = response.Content.
            if (response.IsSuccessStatusCode == true)
            {
                return await response.Content.ReadFromJsonAsync<AccountsDTO>();
            }
            return null;
        }

        public async Task<bool> UpdateAccountUser(AccountsPasswordsDTO account)
        {
            string s = "/api/Accounts/UpdateUser";
            var response = await _httpClient.PostAsJsonAsync(s, account);
            if (response.IsSuccessStatusCode == true)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateAccoutAdmin(AccountsDTO account)
        {
            string s = "/api/Accounts/UpdateAdmin";
            var response = await _httpClient.PostAsJsonAsync(s, account);
            if (response.IsSuccessStatusCode == true)
            {
                return true;
            }
            return false;
        }
    }
}
