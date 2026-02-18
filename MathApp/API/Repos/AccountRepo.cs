using MathApp.Backend.API.Interfaces;
using MathApp.Backend.Data.Enteties;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MathApp.Backend.API.Repos
{
    public class AccountRepo : IAccountRepo
    {
        private readonly DataBase _context;

        public AccountRepo(DataBase context) 
        {
            _context = context;
        }

        public async Task<Account> AddAccount(Account account)
        {
            var accountName = await _context.Accounts.AnyAsync(acc => acc.Username == account.Username);
            var accountEmail = await _context.Accounts.AnyAsync(acc => acc.Email == account.Email);

            if(accountName == true || accountEmail == true)
            {
                return null;
            }

            await _context.Accounts.AddAsync(account);
            _context.SaveChanges();
            return account;
        }

        public async Task<Account> AddAccount(string name, string password, string email, string salt, bool active, string role)
        {
            var accountName = await _context.Accounts.AnyAsync(acc => acc.Username == name);
            var accountEmail = await _context.Accounts.AnyAsync(acc => acc.Email == email);

            if (accountName == true || accountEmail == true)
            {
                return null;
            }


            var account = new Account { Username = name, Email = email, Password = password, Salt = salt, isActive = active, Role = role };
            await _context.Accounts.AddAsync(account);
            _context.SaveChanges();

            return account;
        }

       /* public async Task DeleteAccount(int id)
        {
            var account = await _context.Accounts.ToListAsync();

            foreach (var acc in account)
            {
                if(acc.Id == id)
                {
                    _context.Remove(acc);
                    _context.SaveChanges();
                }
            }
        }
       */
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

        public async Task<bool> UpdateAccountAdmin(string username, string role, bool acctive)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(acc => acc.Username == username);
            
            if (account == null)
            {
                return false;
            }

            account.Role = role;
            account.isActive = acctive;

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> UpdateAccountUser(string username, string email, string password)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(acc => acc.Username == username);

            if (account == null)
            {
                return false;
            }

            account.Email = email;
            account.Password = password;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
