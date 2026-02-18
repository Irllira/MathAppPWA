using MathApp.Backend.Data.Enteties;

namespace MathApp.Backend.API.Interfaces
{
    public interface IAccountRepo
    {
        Task<IEnumerable<Account>> GetAllAccounts();
        Task<Account>? GetAccountById(int id);
        Task<Account>? GetAccountByName(string name);
        Task<Account>? GetAccountByEmail(string email);

        Task<Account> AddAccount(Account account);
        Task<Account> AddAccount(string name, string password, string email, string salt, bool active, string role);
        Task<bool> UpdateAccountAdmin(string username, string role, bool acctive);
        Task<bool> UpdateAccountUser(string username, string email, string password);
       // Task DeleteAccount(int id);
        
    }
}
