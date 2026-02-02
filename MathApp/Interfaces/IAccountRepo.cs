using MathApp.Enteties;

namespace MathEducationWebApp.Components.Interfaces
{
    public interface IAccountRepo
    {
        Task<IEnumerable<Account>> GetAllAccounts();
        Task<Account>? GetAccountById(int id);
        Task<Account>? GetAccountByName(string name);
        Task<Account>? GetAccountByEmail(string email);

        Task AddAccount(Account account);
        Task AddAccount(string name, string password, string email, string salt, bool active, string role);
        Task UpdateAccountAdmin(string username, string role, bool acctive);
        Task UpdateAccountUser(string username, string email, string password);
        Task DeleteAccount(int id);
        
    }
}
