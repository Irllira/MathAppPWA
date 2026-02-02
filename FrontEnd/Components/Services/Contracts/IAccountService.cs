using DTO.DTOs;

namespace FrontEnd.Components.Services.Contracts
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountsDTO>> GetAccounts();
        Task<IEnumerable<AccountsPasswordsDTO>> GetAccountsPasswords();
        Task<AccountsPasswordsDTO>? GetAccountPasswordsByName(string name);
        Task<AccountsDTO>? GetAccountAdminByName(string name);
        Task<AccountsDTO>? GetAccountAdminByEmail(string email);

        Task<bool> AddAccount(AccountsPasswordsDTO account);
        Task<bool> UpdateAccoutAdmin(AccountsDTO account);
        Task<bool> UpdateAccountUser(AccountsPasswordsDTO account);
    }
}
