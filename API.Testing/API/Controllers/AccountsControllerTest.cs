using AutoFixture;
using DTO.DTOs;
using MathApp.Backend.API.Controllers;
using MathApp.Backend.API.Interfaces;
using MathApp.Backend.Data.Enteties;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MathApp.Testing.API.Controllers.Tests;

[TestClass]
public sealed class AccountsControllerTest
{
    private Mock<IAccountRepo> _accountRepoMock;
    private Fixture _fixture;
    private AccountsController _controller;


    public AccountsControllerTest()
    {
        _accountRepoMock = new Mock<IAccountRepo>();
        _fixture = new Fixture();
    }
//  Gets
    [TestMethod]
    public async Task GetAccounts_Correct()
    {
        var accounts = _fixture.CreateMany<Account>(5).ToList();
        _accountRepoMock.Setup(repo => repo.GetAllAccounts()).ReturnsAsync(accounts);
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.GetAccounts();
        var objectResult = result.Result as ObjectResult;

       var acc = objectResult?.Value as IEnumerable<AccountsDTO>;

        Assert.AreEqual(200, objectResult?.StatusCode);
        Assert.AreEqual(5,acc?.Count());
    }

    [TestMethod]
    public async Task GetAccounts_Exeption()
    {
        _accountRepoMock.Setup(repo => repo.GetAllAccounts()).ThrowsAsync(new Exception("Test exception"));
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.GetAccounts();
        var objectResult = result.Result as ObjectResult;

        Assert.AreEqual(400, objectResult?.StatusCode);         
    }

    [TestMethod]
    public async Task GetAccounts_Empty()
    {
       List<Account>? accounts = null;
        _accountRepoMock.Setup(repo => repo.GetAllAccounts()).ReturnsAsync(accounts);
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.GetAccounts();
        var objectResult = result.Result as StatusCodeResult;

        Assert.AreEqual(404, objectResult?.StatusCode);
    }

    [TestMethod]
    public async Task GetAccountsPasswords_Correct()
    {
        var accounts = _fixture.CreateMany<Account>(5).ToList();
        _accountRepoMock.Setup(repo => repo.GetAllAccounts()).ReturnsAsync(accounts);
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.GetAccountsPasswords();
        var objectResult = result.Result as ObjectResult;

        var acc = objectResult?.Value as IEnumerable<AccountsPasswordsDTO>;

        Assert.AreEqual(200, objectResult?.StatusCode);
        Assert.AreEqual(5, acc?.Count());
    }

    [TestMethod]
    public async Task GetAccountsPasswords_Exeption()
    {
        _accountRepoMock.Setup(repo => repo.GetAllAccounts()).ThrowsAsync(new Exception("Test exception"));
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.GetAccountsPasswords();
        var objectResult = result.Result as ObjectResult;

        Assert.AreEqual(400, objectResult?.StatusCode);
    }

    [TestMethod]
    public async Task GetAccountsPasswords_Empty()
    {
        List<Account>? accounts = null;
        _accountRepoMock.Setup(repo => repo.GetAllAccounts()).ReturnsAsync(accounts);
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.GetAccountsPasswords();
        var objectResult = result.Result as StatusCodeResult;

        Assert.AreEqual(404, objectResult?.StatusCode);
    }


    [TestMethod]
    public async Task GetAccountPasswordByName_Correct()
    {
        var accounts = _fixture.CreateMany<Account>(5).ToList();

        Account account = accounts[0];
        string name = account.Username;
        _accountRepoMock.Setup(repo => repo.GetAccountByName(name)).ReturnsAsync(account);
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.GetAccountPasswordByName(name);
        var objectResult = result.Result as ObjectResult;

        var acc = objectResult?.Value as AccountsPasswordsDTO;

        Assert.AreEqual(200, objectResult?.StatusCode);
        Assert.AreEqual(account.Username, acc?.Username);
        Assert.AreEqual(account.Email, acc?.Email);
        Assert.AreEqual(account.Salt, acc?.Salt);
        Assert.AreEqual(account.Password, acc?.Password);
    }

    [TestMethod]
    public async Task GetAccountPasswordByName_Exeption()
    {
        _accountRepoMock.Setup(repo => repo.GetAccountByName("test")).ThrowsAsync(new Exception("Test exception"));
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.GetAccountPasswordByName("test");
        var objectResult = result.Result as ObjectResult;

        Assert.AreEqual(400, objectResult?.StatusCode);
    }


    [TestMethod]
    public async Task GetAccountPasswordByName_Empty()
    {
        Account? accounts = null;
        _accountRepoMock.Setup(repo => repo.GetAccountByName("test")).ReturnsAsync(accounts);
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.GetAccountPasswordByName("test");
        var objectResult = result.Result as StatusCodeResult;

        Assert.AreEqual(404, objectResult?.StatusCode);
    }

    [TestMethod]
    public async Task GetAccountAdminByName_Correct()
    {
        var accounts = _fixture.CreateMany<Account>(5).ToList();

        Account account = accounts[0];
        string name = account.Username;
        _accountRepoMock.Setup(repo => repo.GetAccountByName(name)).ReturnsAsync(account);
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.GetAccountAdminByName(name);
        var objectResult = result.Result as ObjectResult;

        var acc = objectResult?.Value as AccountsDTO;

        Assert.AreEqual(200, objectResult?.StatusCode);
        Assert.AreEqual(account.Username, acc?.Username);
        Assert.AreEqual(account.Email, acc?.Email);
        Assert.AreEqual(account.Role, acc?.Role);
        Assert.AreEqual(account.isActive, acc?.isActive);
    }

    [TestMethod]
    public async Task GetAccountAdminByName_Exeption()
    {
        _accountRepoMock.Setup(repo => repo.GetAccountByName("test")).ThrowsAsync(new Exception("Test exception"));
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.GetAccountAdminByName("test");
        var objectResult = result.Result as ObjectResult;

        Assert.AreEqual(400, objectResult?.StatusCode);
    }


    [TestMethod]
    public async Task GetAccountAdminByName_Empty()
    {
        Account? accounts = null;
        _accountRepoMock.Setup(repo => repo.GetAccountByName("test")).ReturnsAsync(accounts);
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.GetAccountAdminByName("test");
        var objectResult = result.Result as StatusCodeResult;

        Assert.AreEqual(404, objectResult?.StatusCode);
    }

    [TestMethod]
    public async Task GetAccountAdminByEmail_Correct()
    {
        var accounts = _fixture.CreateMany<Account>(5).ToList();

        Account account = accounts[0];
        string email = account.Email;
        _accountRepoMock.Setup(repo => repo.GetAccountByEmail(email)).ReturnsAsync(account);
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.GetAccountAdminByEmail(email);
        var objectResult = result.Result as ObjectResult;

        var acc = objectResult?.Value as AccountsDTO;

        Assert.AreEqual(200, objectResult?.StatusCode);
        Assert.AreEqual(account.Username, acc?.Username);
        Assert.AreEqual(account.Email, acc?.Email);
        Assert.AreEqual(account.Role, acc?.Role);
        Assert.AreEqual(account.isActive, acc?.isActive);
    }

    [TestMethod]
    public async Task GetAccountAdminByEmail_Exeption()
    {
        _accountRepoMock.Setup(repo => repo.GetAccountByEmail("test")).ThrowsAsync(new Exception("Test exception"));
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.GetAccountAdminByEmail("test");
        var objectResult = result.Result as ObjectResult;

        Assert.AreEqual(400, objectResult?.StatusCode);
    }


    [TestMethod]
    public async Task GetAccountAdminByEmail_Empty()
    {
        Account? accounts = null;
        _accountRepoMock.Setup(repo => repo.GetAccountByEmail("test")).ReturnsAsync(accounts);
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.GetAccountAdminByEmail("test");
        var objectResult = result.Result as StatusCodeResult;

        Assert.AreEqual(404, objectResult?.StatusCode);
    }
    // Posts
    [TestMethod]
    public async Task AddAccount_Correct()
    {
        var account = _fixture.Create<Account>();
        AccountsPasswordsDTO accountDTO = new AccountsPasswordsDTO()
        {
            Username = account.Username,
            Email = account.Email,
            Password = account.Password,
            Salt = account.Salt,
            Role = account.Role,
            isActive = account.isActive,
        };

        _accountRepoMock.Setup(repo => repo.AddAccount(It.IsAny<Account>())).ReturnsAsync(account);
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.AddAccount(accountDTO);
        var objectResult = result.Result as ObjectResult;

        var acc = objectResult?.Value as AccountsPasswordsDTO;

        Assert.AreEqual(201, objectResult?.StatusCode); 
        Assert.AreEqual(account.Username, acc?.Username);
        Assert.AreEqual(account.Email, acc?.Email);
        Assert.AreEqual(account.Salt, acc?.Salt);
        Assert.AreEqual(account.Password, acc?.Password);
    }

    [TestMethod]
    public async Task AddAccount_Exeption()
    {
        var account = _fixture.CreateMany<AccountsPasswordsDTO>(1).ToList()[0];

        _accountRepoMock.Setup(repo => repo.AddAccount(It.IsAny<Account>())).ThrowsAsync(new Exception("Test exception"));
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.AddAccount(account);
        var objectResult = result.Result as ObjectResult;

        Assert.AreEqual(400, objectResult?.StatusCode);
    }

    [TestMethod]
    public async Task AddAccount_Empty()
    {
        var account = _fixture.CreateMany<AccountsPasswordsDTO>(1).ToList()[0];

        Account? accounts = null;
        _accountRepoMock.Setup(repo => repo.AddAccount(It.IsAny<Account>())).ReturnsAsync(accounts);
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.AddAccount(account);
        var objectResult = result.Result as StatusCodeResult;

        Assert.AreEqual(404, objectResult?.StatusCode);
    }


    [TestMethod]
    public async Task UpdateAdmin_Correct()
    {
        var accountDTO = _fixture.CreateMany<AccountsDTO>(1).ToList()[0];
      

        _accountRepoMock.Setup(repo => repo.UpdateAccountAdmin(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(true);
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.UpdateAdmin(accountDTO);
        var objectResult = result.Result as ObjectResult;

        var acc = objectResult?.Value as Account;

        Assert.AreEqual(200, objectResult?.StatusCode);
    }

    [TestMethod]
    public async Task UpdateAdmin_Exeption()
    {
        var account = _fixture.CreateMany<AccountsDTO>(1).ToList()[0];
        _accountRepoMock.Setup(repo => repo.UpdateAccountAdmin(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).ThrowsAsync(new Exception("Test exception"));
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.UpdateAdmin(account);
        var objectResult = result.Result as ObjectResult;

        Assert.AreEqual(400, objectResult?.StatusCode);
    }

    [TestMethod]
    public async Task UpdateAdmin_Empty()
    {
        var account = _fixture.Create<AccountsDTO>();

        _accountRepoMock.Setup(repo => repo.UpdateAccountAdmin(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(false);
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.UpdateAdmin(account);
        var objectResult = result.Result as StatusCodeResult;

        Assert.AreEqual(404, objectResult?.StatusCode);
    }


    [TestMethod]
    public async Task UpdateUser_Correct()
    {
        var accountDTO = _fixture.CreateMany<AccountsPasswordsDTO>(1).ToList()[0];


        _accountRepoMock.Setup(repo => repo.UpdateAccountUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.UpdateUser(accountDTO);
        var objectResult = result.Result as ObjectResult;

        var acc = objectResult?.Value as Account;

        Assert.AreEqual(200, objectResult?.StatusCode);
    }

    [TestMethod]
    public async Task UpdateUser_Exeption()
    {
        var accountDTO = _fixture.CreateMany<AccountsPasswordsDTO>(1).ToList()[0];
        _accountRepoMock.Setup(repo => repo.UpdateAccountUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception("Test exception"));
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.UpdateUser(accountDTO);
        var objectResult = result.Result as ObjectResult;

        Assert.AreEqual(400, objectResult?.StatusCode);
    }

    [TestMethod]
    public async Task UpdateUser_Empty()
    {
        var accountDTO = _fixture.CreateMany<AccountsPasswordsDTO>(1).ToList()[0];

        Account? accounts = null;
        _accountRepoMock.Setup(repo => repo.UpdateAccountUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
        _controller = new AccountsController(_accountRepoMock.Object);

        var result = await _controller.UpdateUser(accountDTO);
        var objectResult = result.Result as StatusCodeResult;

        Assert.AreEqual(404, objectResult?.StatusCode);
    }
}
