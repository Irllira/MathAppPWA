using AutoFixture;
using MathApp.Backend.API.Repos;
using MathApp.Backend.Data.Enteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MathApp.Testing.API.Repos.Tests
{
    [TestClass()]
    public class AccountRepoTest
    {
        private DbContextOptions<DataBase> _options;
        private Fixture _fixture;

        [TestInitialize]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<DataBase>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _fixture = new Fixture();
        
        }

        [TestMethod()]
        public async Task AddAccount_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new AccountRepo(context);
            var newAccount = _fixture.Create<Account>();

            var result = await repository.AddAccount(newAccount);
            
            Assert.IsNotNull(result);
            Assert.AreEqual(newAccount.Username, result.Username);
            Assert.AreEqual(1, context.Accounts.Count());
        }

        [TestMethod()]
        public async Task AddAccount_RepeatUsername()
        {
            using var context = new DataBase(_options);
            var repository = new AccountRepo(context);
            var newAccount = _fixture.Create<Account>();
            context.Accounts.Add(newAccount);
            context.SaveChanges();
            newAccount.Email = "difEmail";

            var result = await repository.AddAccount(newAccount);

            Assert.IsNull(result);
            Assert.AreEqual(1, context.Accounts.Count());
        }
        [TestMethod()]
        public async Task AddAccount_RepeatEmail()
        {
            using var context = new DataBase(_options);
            var repository = new AccountRepo(context);
            var newAccount = _fixture.Create<Account>();
            await context.Accounts.AddAsync(newAccount);
            context.SaveChanges();
            newAccount.Username = "difName";

            var result = await repository.AddAccount(newAccount);

            Assert.IsNull(result);
            Assert.AreEqual(1, context.Accounts.Count());
        }
        [TestMethod()]
        public async Task GetAccountById_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new AccountRepo(context);
            var Accounts = _fixture.CreateMany<Account>(5);
            await context.Accounts.AddRangeAsync(Accounts);
            context.SaveChanges();

            var account = Accounts.ToList()[1];

            var result = await repository.GetAccountById(account.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(Accounts.ToList()[1].Username, result.Username);
        }
        [TestMethod()]
        public async Task GetAccountById_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new AccountRepo(context);
            var Accounts = _fixture.CreateMany<Account>(2).ToList();
            Accounts[0].Id = 1;
            Accounts[1].Id = 2;
            await context.Accounts.AddRangeAsync(Accounts);
            context.SaveChanges();
          
            var result = await repository.GetAccountById(3);

            Assert.IsNull(result);
        }

        [TestMethod()]
        public async Task GetAccountByName_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new AccountRepo(context);
            var Accounts = _fixture.CreateMany<Account>(5);
            await context.Accounts.AddRangeAsync(Accounts);
            context.SaveChanges();

            var account = Accounts.ToList()[1];

            var result = await repository.GetAccountByName(account.Username);

            Assert.IsNotNull(result);
            Assert.AreEqual(Accounts.ToList()[1].Username, result.Username);
        }

        [TestMethod()]
        public async Task GetAccountByName_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new AccountRepo(context);
            var Accounts = _fixture.CreateMany<Account>(2).ToList();
            Accounts[0].Username = "Hello";
            Accounts[0].Username = "World";

            await context.Accounts.AddRangeAsync(Accounts);
            context.SaveChanges();

            var result = await repository.GetAccountByName("Test");

            Assert.IsNull(result);
        }

       //GetAccountByEmail isn't tested as it is not used by the FrontEnd or the controllers

        [TestMethod()]
        public async Task GetAllAccounts_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new AccountRepo(context);
            var Accounts = _fixture.CreateMany<Account>(5);
            await context.Accounts.AddRangeAsync(Accounts);
            context.SaveChanges();

            var result = await repository.GetAllAccounts();

            Assert.IsNotNull(result);
            Assert.AreEqual(Accounts.ToList()[1].Username, result.ToList()[1].Username);
            Assert.AreEqual(5, context.Accounts.Count());
        }

        [TestMethod()]
        public async Task GetAllAccounts_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new AccountRepo(context);
            var Accounts = _fixture.CreateMany<Account>(5);

            var result = await repository.GetAllAccounts();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, context.Accounts.Count());
        }

        [TestMethod()]
        public async Task UpdateAccountAdmin_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new AccountRepo(context);
            var newAccount = _fixture.Create<Account>();
            context.Accounts.Add(newAccount);
            context.SaveChanges();

            var editAccount = _fixture.Create<Account>(); 
            editAccount.Username = newAccount.Username;
            editAccount.Role = "NewRole";

            var result = await repository.UpdateAccountAdmin(editAccount.Username, editAccount.Role, editAccount.isActive);

            Assert.IsTrue( result);
            var updated = await context.Accounts.FirstAsync(a => a.Username == newAccount.Username);
            Assert.AreEqual("NewRole", updated.Role);
        }
        [TestMethod()]
        public async Task UpdateAccountAdmin_DoesntExist()
        {
            using var context = new DataBase(_options);
            var repository = new AccountRepo(context);
            var newAccount = _fixture.Create<Account>();
            newAccount.Username = "OldUsername";
            newAccount.Role = "OldRole";
            context.Accounts.Add(newAccount);
            context.SaveChanges();

            var editAccount = _fixture.Create<Account>();
            editAccount.Username = "NewUsername";
            editAccount.Role = "NewRole";


            var result = await repository.UpdateAccountAdmin(editAccount.Username, editAccount.Role, editAccount.isActive);

            Assert.IsFalse(result);
            var updated = await context.Accounts.FirstAsync(a => a.Username == newAccount.Username);
            Assert.AreNotEqual("NewRole", updated.Role);
        }


        [TestMethod()]
        public async Task UpdateAccountUser_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new AccountRepo(context);
            var newAccount = _fixture.Create<Account>();
            newAccount.Email = "OldEmail";
            context.Accounts.Add(newAccount);
            context.SaveChanges();

            var editAccount = _fixture.Create<Account>();
            editAccount.Username = newAccount.Username;
            editAccount.Email = "NewEmail";

            var result = await repository.UpdateAccountUser(editAccount.Username, editAccount.Email, editAccount.Password);

            Assert.IsTrue(result);
            var updated = await context.Accounts.FirstAsync(a => a.Username == newAccount.Username);
            Assert.AreEqual("NewEmail", updated.Email);
        }

        [TestMethod()]
        public async Task UpdateAccountUser_DoesntExist()
        {
            using var context = new DataBase(_options);
            var repository = new AccountRepo(context);
            var newAccount = _fixture.Create<Account>();
            newAccount.Username = "OldName";
            newAccount.Email = "OldEmail";
            context.Accounts.Add(newAccount);
            context.SaveChanges();

            var editAccount = _fixture.Create<Account>();
            editAccount.Username = "NewName";
            editAccount.Email = "NewEmail";

            var result = await repository.UpdateAccountUser(editAccount.Username, editAccount.Email, editAccount.Password);

            Assert.IsFalse(result);
            var updated = await context.Accounts.FirstAsync(a => a.Username == newAccount.Username);
            Assert.AreNotEqual("NewEmail", updated.Email);
        }
    }


}