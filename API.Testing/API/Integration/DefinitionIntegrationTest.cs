using DTO.DTOs;
using FluentAssertions;
using MathApp.Testing.API.Integration;
using System.Net.Http.Json;

namespace MathApp.Testing.API.Integration.Tests;

[TestClass]
public class DefinitionIntegrationTest
{
    [TestMethod]
    public async Task AddDefinition_Integration_Correct()
    {
        //  Arrange
        var application = new MathAppWebApplicationFactory();

        var addRequest = new DefinitionDTO()
        {
            ID = 0,
            name = "Test Definition",
            type = "Test Type",
            part1 = "Part 1",
            part2 = "Part 2",
            UnitName = "Test Unit"
        };
        var addUnit = new UnitDTO()
        {
            ID = 0,
            name = "Test Unit",
            description = "Test Description",
            educationLevel = "Test Education Level"
        };
      

        var _client = application.CreateClient();
        var responseAddUnit = await _client.PostAsJsonAsync("/api/Unit", addUnit);
        responseAddUnit.EnsureSuccessStatusCode();

        //  Act
        var response = await _client.PostAsJsonAsync("/api/Definition", addRequest);

        //  Assert
        response.EnsureSuccessStatusCode();
        var addedDef = await response.Content.ReadFromJsonAsync<DefinitionDTO>();
        Assert.IsNotNull(addedDef);
        Assert.AreEqual(addRequest.UnitName, addedDef.UnitName);
    }

    [TestMethod]
    public async Task RemoveDefinition_Integration_Correct()
    {
        //  Arrange
        var application = new MathAppWebApplicationFactory();

        var addRequest = new DefinitionDTO()
        {
            ID = 0,
            name = "Test Definition",
            type = "Test Type",
            part1 = "Part 1",
            part2 = "Part 2",
            UnitName = "Test Unit"
        };
        var addUnit = new UnitDTO()
        {
            ID = 0,
            name = "Test Unit",
            description = "Test Description",
            educationLevel = "Test Education Level"
        };


        var _client = application.CreateClient();
        var responseAddUnit = await _client.PostAsJsonAsync("/api/Unit", addUnit);
        responseAddUnit.EnsureSuccessStatusCode();

        var responseAddDef = await _client.PostAsJsonAsync("/api/Definition", addRequest);
        responseAddDef.EnsureSuccessStatusCode();
        //  Act

        var response = await _client.DeleteAsync("/api/Definition/Delete/0");

        //  Assert
        response.EnsureSuccessStatusCode();
        
    }

    [TestMethod]
    public async Task RemoveDefinition_Integration_DoesntExist()
    {
        //  Arrange
        var application = new MathAppWebApplicationFactory();

        var _client = application.CreateClient();  
      
        //  Act

        var response = await _client.DeleteAsync("/api/Definition/Delete/0");

        //  Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);

    }

    [TestMethod]
    public async Task AddUpdate_Integration_Correct()
    {
        //  Arrange
        var application = new MathAppWebApplicationFactory();

        var addRequest = new DefinitionDTO()
        {
            ID = 0,
            name = "Test Definition",
            type = "Test Type",
            part1 = "Part 1",
            part2 = "Part 2",
            UnitName = "Test Unit"
        };
        var addUnit = new UnitDTO()
        {
            ID = 0,
            name = "Test Unit",
            description = "Test Description",
            educationLevel = "Test Education Level"
        };
        var editRequest = new DefinitionDTO()
        {
            ID = 0,
            name = "Updated Definition",
            type = "Test Type",
            part1 = "Part 1",
            part2 = "Part 2",
            UnitName = "Test Unit"
        };

        var _client = application.CreateClient();
        var responseAddUnit = await _client.PostAsJsonAsync("/api/Unit", addUnit);
        responseAddUnit.EnsureSuccessStatusCode();
        var responseAddDef = await _client.PostAsJsonAsync("/api/Definition", addRequest);
        responseAddDef.EnsureSuccessStatusCode();

        //  Act
        var response = await _client.PostAsJsonAsync("/api/Definition", editRequest);


        //  Assert
        response.EnsureSuccessStatusCode();
        var updated = await response.Content.ReadFromJsonAsync<DefinitionDTO>();
        Assert.IsNotNull(updated);
        Assert.AreEqual(editRequest.UnitName, updated.UnitName);
    }

    [TestMethod]
    public async Task AddUpdate_Integration_DoesntExist()
    {
        //  Arrange
        var application = new MathAppWebApplicationFactory();

      
        var editRequest = new DefinitionDTO()
        {
            ID = 0,
            name = "Updated Definition",
            type = "Test Type",
            part1 = "Part 1",
            part2 = "Part 2",
            UnitName = "Test Unit"
        };

        var _client = application.CreateClient();
        
        //  Act
        var response = await _client.PostAsJsonAsync("/api/Definition", editRequest);


        //  Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);

    }
}
