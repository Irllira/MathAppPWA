using AutoFixture;
using Azure;
using DTO.DTOs;
using FluentAssertions;
using MathApp.Backend.Data.Enteties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;

namespace MathApp.Testing.API.Integration.Tests;

[TestClass]
public class AccountIntegrationTest
{

    [TestMethod]
    public async Task AddAccount_Integration_Correct()
    {
        //  Arrange
        var application = new MathAppWebApplicationFactory();
        
        var addRequest = new AccountsPasswordsDTO()
        {
            Username = "User123",
            Password = "NewSecurePassword!2024",
            Salt = "Random",
            Email = "email",
            Role = "Admin",
            isActive = true
        };

        var _client = application.CreateClient();

        //  Act
        var response = await _client.PostAsJsonAsync("/api/Accounts/AddAccount", addRequest);

        //  Assert
        response.EnsureSuccessStatusCode();
        var addedAccount = await response.Content.ReadFromJsonAsync<AccountsPasswordsDTO>();
        Assert.IsNotNull(addedAccount);
        Assert.AreEqual(addRequest.Username, addedAccount.Username);
    }

    [TestMethod]
    public async Task UpdateAccountUser_Integration_Correct()
    {
        //  Arrange
        var application = new MathAppWebApplicationFactory();

        var _client = application.CreateClient();

        var addRequest = new AccountsPasswordsDTO()
        {
            Username = "User123",
            Password = "BadPass",
            Salt = "Random",
            Email = "email",
            Role = "Admin",
            isActive = true
        };    

        var responseadd = await _client.PostAsJsonAsync("/api/Accounts/AddAccount", addRequest);
        responseadd.EnsureSuccessStatusCode();

        var updateRequest = new AccountsPasswordsDTO()
        {
            Username = "User123",
            Password = "G00dS3cuRePa$$word",
            Salt = "Random",
            Email = "email",
            Role = "Admin",
            isActive = true
        };

        //  Act
        var responseUser = await _client.PostAsJsonAsync("/api/Accounts/UpdateUser", updateRequest);


        //  Assert
        responseUser.EnsureSuccessStatusCode();
        var updatedAccount = await responseUser.Content.ReadFromJsonAsync<AccountsPasswordsDTO>();
        Assert.IsNotNull(updatedAccount);
        Assert.AreEqual(updateRequest.Username, updatedAccount.Username);
        Assert.AreEqual(updateRequest.Password, updatedAccount.Password);
        Assert.AreNotEqual(addRequest.Password, updatedAccount.Password);
    }

    [TestMethod]
    public async Task UpdateAccountAdmin_Integration_Correct()
    {
        //  Arrange
        var application = new MathAppWebApplicationFactory();

        var _client = application.CreateClient();

        var addRequest = new AccountsPasswordsDTO()
        {
            Username = "User123",
            Password = "BadPass",
            Salt = "Random",
            Email = "email",
            Role = "Admin",
            isActive = true
        };

        var responseadd = await _client.PostAsJsonAsync("/api/Accounts/AddAccount", addRequest);
        responseadd.EnsureSuccessStatusCode();

        var updateRequest = new AccountsDTO()
        {
            Username = "User123",
            Email = "email",
            Role = "User",
            isActive = true
        };

        //  Act
        var responseAdmin = await _client.PostAsJsonAsync("/api/Accounts/UpdateAdmin", updateRequest);


        //  Assert
        responseAdmin.EnsureSuccessStatusCode();
        var updatedAccount = await responseAdmin.Content.ReadFromJsonAsync<AccountsDTO>();
        Assert.IsNotNull(updatedAccount);
        Assert.AreEqual(updateRequest.Username, updatedAccount.Username);
        Assert.AreEqual(updateRequest.Role, updatedAccount.Role);
        Assert.AreNotEqual(addRequest.Role, updatedAccount.Role);
    }

    [TestMethod]
    public async Task AddAccount_Integration_WrongDTO()
    {
        //  Arrange
        var application = new MathAppWebApplicationFactory();

        var addRequest = new AccountsDTO()
        {
            Username = "User123",
            Email = "email",
            Role = "Admin",
            isActive = true
        };

        var _client = application.CreateClient();

        //  Act
        var response = await _client.PostAsJsonAsync("/api/Accounts/AddAccount", addRequest);

        //  Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      
    }

    [TestMethod]
    public async Task UpdateAccountUser_Integration_WrongDTO()
    {
        //  Arrange
        var application = new MathAppWebApplicationFactory();

        var _client = application.CreateClient();

        var addRequest = new AccountsPasswordsDTO()
        {
            Username = "User123",
            Password = "BadPass",
            Salt = "Random",
            Email = "email",
            Role = "Admin",
            isActive = true
        };

        var responseadd = await _client.PostAsJsonAsync("/api/Accounts/AddAccount", addRequest);
        responseadd.EnsureSuccessStatusCode();

        var updateRequest = new AccountsDTO()
        {
            Username = "User123",
            
            Email = "email",
            Role = "Admin2",
            isActive = true
        };

        //  Act
        var responseUser = await _client.PostAsJsonAsync("/api/Accounts/UpdateUser", updateRequest);


        //  Assert
        responseUser.StatusCode.Should().Be(HttpStatusCode.BadRequest);

    }

}

