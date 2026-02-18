using API.Enteties;
using DTO.DTOs;
using FluentAssertions;
using MathApp.Testing.API.Integration;
using System.Net.Http.Json;

namespace MathApp.Testing.API.Integration.Tests;

[TestClass]
public class IncorrectIntegrationTest
{

    [TestMethod]
    public async Task AddIncorrect_Integration_Correct()
    {
        //  Arrange
        var application = new MathAppWebApplicationFactory();

        var incorrect = new IncorrectDTO()
        {
           content = "This is incorrect"
        };
        var _client = application.CreateClient();

        //  Act
        var response = await _client.PostAsJsonAsync("/api/Incorrect/NewIncorrect", incorrect);

        //  Assert
        response.EnsureSuccessStatusCode();
        var addedIncorr = await response.Content.ReadFromJsonAsync<IncorrectDTO>();
        Assert.IsNotNull(addedIncorr);
        Assert.AreEqual(incorrect.content, addedIncorr.content);
    }

    [TestMethod]
    public async Task AddPair_Integration_Correct()
    {
        //  Arrange
        var application = new MathAppWebApplicationFactory();

        var incorrect = new IncorrectDTO()
        {
            content = "This is incorrect"
        };

        var unit = new UnitDTO()
        {
            name = "Unit 1",
            ID = 0,
            educationLevel = "Test Education Level",
            description = "Description for Unit 1",
        };
        var def = new DefinitionDTO()
        {
            name= "Definition for incorrect",
            part1 = "Part 1",
            part2 = "Part 2",
            UnitName = "Unit 1",
            ID = 0
        };

        var pair = new DefIncPairDTO()
        {
            DefinitionID = 1,
            IncorrectID = 1
        };
        var _client = application.CreateClient();

        var responseUnit = await _client.PostAsJsonAsync("/api/Unit", unit);
        responseUnit.EnsureSuccessStatusCode();
        var responseInc = await _client.PostAsJsonAsync("/api/Incorrect/NewIncorrect", incorrect);
        responseInc.EnsureSuccessStatusCode();
        var responseDef = await _client.PostAsJsonAsync("/api/Definition", def);
        responseDef.EnsureSuccessStatusCode();

        //  Act
        var response = await _client.PostAsJsonAsync("/api/Incorrect/AddPair", pair);

        //  Assert
        response.EnsureSuccessStatusCode();
    }

    [TestMethod]
    public async Task AddPair_Integration_DosentExist()
    {
        //  Arrange
        var application = new MathAppWebApplicationFactory();

        var incorrect = new IncorrectDTO()
        {
            content = "This is incorrect"
        };       

        var pair = new DefIncPairDTO()
        {
            DefinitionID = 1,
            IncorrectID = 1
        };
        var _client = application.CreateClient();

        //  Act
        var response = await _client.PostAsJsonAsync("/api/Incorrect/AddPair", pair);

        //  Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}
