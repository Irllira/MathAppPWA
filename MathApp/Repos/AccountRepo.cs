using MathApp.Enteties;
using MathEducationWebApp.Components.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MathEducationWebApp.Components.Nowy_folder
{
    public class AccountRepo : IAccountRepo
    {
        private readonly DataBase _context;

        public AccountRepo(DataBase context) 
        {
            _context = context;
        }

        public async Task AddAccount(Account account)
        {
            await _context.Accounts.AddAsync(account);
            _context.SaveChanges();
        }

        public async Task AddAccount(string name, string password, string email, string salt, bool active, string role)
        {
            await _context.Accounts.AddAsync(new Account { Username = name, Email = email, Password = password, Salt = salt, isActive = active, Role = role });
            _context.SaveChanges();
        }

        public async Task DeleteAccount(int id)
        {
            var account = await _context.Accounts.ToListAsync();

            foreach (var acc in account)
            {
                if(acc.Id == id)
                {
                    _context.Remove<Account>(acc);
                    _context.SaveChanges();
                }
            }
        }

        public async Task<Account>? GetAccountById(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            return account;
        }

        public async Task<Account>? GetAccountByName(string name)
        {
            var accounts = await _context.Accounts.ToListAsync();
            
            foreach(var a in accounts)
            {
                if (a.Username == name)
                    return a;
            }

            return null;
        }

        public async Task<Account>? GetAccountByEmail(string email)
        {
            var accounts = await _context.Accounts.ToListAsync();

            foreach (var a in accounts)
            {
                if (a.Email == email)
                    return a;
            }
            return null;
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            var accounts = await _context.Accounts.ToListAsync();
            return accounts;
        }

        public async Task UpdateAccountAdmin(string username, string role, bool acctive)
        {
            var account = await _context.Accounts.Where(acc => acc.Username == username).ExecuteUpdateAsync(setters => setters
            .SetProperty(acc => acc.Role, role)
            .SetProperty(acc => acc.isActive, acctive));

            await _context.SaveChangesAsync();
        }
        public async Task UpdateAccountUser(string username, string email, string password)
        {
            var account = await _context.Accounts.Where(acc => acc.Username == username).ExecuteUpdateAsync(setters => setters
            .SetProperty(acc => acc.Email, email)
            .SetProperty(acc => acc.Password, password));

            await _context.SaveChangesAsync();
        }
    }
}
