using AutoFixture;
using DTO.DTOs;
using MathApp.Backend.API.Controllers;
using MathApp.Backend.API.Interfaces;
using MathApp.Backend.Data.Enteties;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MathApp.Testing.API.Controllers.Tests;

[TestClass]
public class DefinitionControllerTest
{
    private Mock<IDefinitionRepo> _definitionRepoMock;
    private Mock<IUnitRepo> _unitRepoMock;

    private Fixture _fixture;
    private DefinitionController _controller;


    public DefinitionControllerTest()
    {
        _definitionRepoMock = new Mock<IDefinitionRepo>();
        _unitRepoMock = new Mock<IUnitRepo>();
        _fixture = new Fixture();
    }

    [TestMethod]
    public async Task GetDefinitions_Correct()
    {
        var definitions = _fixture.CreateMany<Definition>(5).ToList();
        var unit = _fixture.Create<Unit>();

        _unitRepoMock.Setup(repo => repo.GetUnitByID(It.IsAny<int>())).ReturnsAsync(unit);
        _definitionRepoMock.Setup(repo => repo.GetAllDefinitions()).ReturnsAsync(definitions);
        _controller = new DefinitionController(_definitionRepoMock.Object, _unitRepoMock.Object);

        var result = await _controller.GetDefinitions();
        var objectResult = result.Result as ObjectResult;

        var def = objectResult?.Value as IEnumerable<DefinitionDTO>;

        Assert.AreEqual(200, objectResult?.StatusCode);
        Assert.AreEqual(5, def?.Count());
    }

    [TestMethod]
    public async Task GetDefinitions_Exeption()
    {
        var unit = _fixture.Create<Unit>();

        _unitRepoMock.Setup(repo => repo.GetUnitByID(It.IsAny<int>())).ReturnsAsync(unit);
        _definitionRepoMock.Setup(repo => repo.GetAllDefinitions()).ThrowsAsync(new Exception("Test exception"));

        _controller = new DefinitionController(_definitionRepoMock.Object, _unitRepoMock.Object);

        var result = await _controller.GetDefinitions();
        var objectResult = result.Result as ObjectResult;

        Assert.AreEqual(400, objectResult?.StatusCode);
    }

    [TestMethod]
    public async Task GetDefinitions_EmptyDefinition()
    {
        List<Definition>? definition = null;
        var unit = _fixture.Create<Unit>();

        _unitRepoMock.Setup(repo => repo.GetUnitByID(It.IsAny<int>())).ReturnsAsync(unit);
        _definitionRepoMock.Setup(repo => repo.GetAllDefinitions()).ReturnsAsync(definition);

        _controller = new DefinitionController(_definitionRepoMock.Object, _unitRepoMock.Object);

        var result = await _controller.GetDefinitions();
        var objectResult = result.Result as StatusCodeResult;

        Assert.AreEqual(404, objectResult?.StatusCode);
    }

    [TestMethod]
    public async Task GetDefinitions_EmptyUnitAll()
    {
        var definitions = _fixture.CreateMany<Definition>(5).ToList();
        Unit unit = null;

        _unitRepoMock.Setup(repo => repo.GetUnitByID(It.IsAny<int>())).ReturnsAsync(unit);
        _definitionRepoMock.Setup(repo => repo.GetAllDefinitions()).ReturnsAsync(definitions);

        _controller = new DefinitionController(_definitionRepoMock.Object, _unitRepoMock.Object);

        var result = await _controller.GetDefinitions();
        var objectResult = result.Result as ObjectResult;

        var def = objectResult?.Value as IEnumerable<DefinitionDTO>;

        Assert.AreEqual(200, objectResult?.StatusCode);
        Assert.AreEqual(0, def?.Count());
    }

    [TestMethod]
    public async Task GetDefinitions_EmptyUnitOne()
    {
        List<Definition> definitions = _fixture.CreateMany<Definition>(3).ToList();
        Unit unitNull = null;
        var unit = _fixture.Create<Unit>();

        _unitRepoMock.Setup(repo => repo.GetUnitByID(definitions[0].unitId)).ReturnsAsync(unit);
        _unitRepoMock.Setup(repo => repo.GetUnitByID(definitions[1].unitId)).ReturnsAsync(unitNull);
        _unitRepoMock.Setup(repo => repo.GetUnitByID(definitions[2].unitId)).ReturnsAsync(unit);

        _definitionRepoMock.Setup(repo => repo.GetAllDefinitions()).ReturnsAsync(definitions);

        _controller = new DefinitionController(_definitionRepoMock.Object, _unitRepoMock.Object);

        var result = await _controller.GetDefinitions();
        var objectResult = result.Result as ObjectResult;

        var def = objectResult?.Value as IEnumerable<DefinitionDTO>;

        Assert.AreEqual(200, objectResult?.StatusCode);
        Assert.AreEqual(2, def?.Count());
    }

    [TestMethod]
    public async Task GetDefinitionsByUnit_Correct()
    {
        var definitions = _fixture.CreateMany<Definition>(5).ToList();
        var unit = _fixture.Create<Unit>();

        _unitRepoMock.Setup(repo => repo.GetUnitByName(unit.name)).ReturnsAsync(unit);
        _definitionRepoMock.Setup(repo => repo.GetDefinitionsByUnit(unit)).ReturnsAsync(definitions);
        _controller = new DefinitionController(_definitionRepoMock.Object, _unitRepoMock.Object);

        var result = await _controller.GetDefinitionsByUnit(unit.name);
        var objectResult = result.Result as ObjectResult;

        var def = objectResult?.Value as IEnumerable<DefinitionDTO>;

        Assert.AreEqual(200, objectResult?.StatusCode);
        Assert.AreEqual(5, def?.Count());
    }

    [TestMethod]
    public async Task GetDefinitionsByUnit_Exeption()
    {
        var unit = _fixture.Create<Unit>();

        _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ThrowsAsync(new Exception("Test exception"));
        _definitionRepoMock.Setup(repo => repo.GetDefinitionsByUnit(It.IsAny<int>())).ThrowsAsync(new Exception("Test exception"));

        _controller = new DefinitionController(_definitionRepoMock.Object, _unitRepoMock.Object);

        var result = await _controller.GetDefinitionsByUnit(unit.name);
        var objectResult = result.Result as ObjectResult;

        Assert.AreEqual(400, objectResult?.StatusCode);

    }

    [TestMethod]
    public async Task GetDefinitionsByUnit_EmptyDefinition()
    {
        List<Definition>? definition = null;
        var unit = _fixture.Create<Unit>();

        _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
        _definitionRepoMock.Setup(repo => repo.GetDefinitionsByUnit(It.IsAny<int>())).ReturnsAsync(definition);

        _controller = new DefinitionController(_definitionRepoMock.Object, _unitRepoMock.Object);

        var result = await _controller.GetDefinitionsByUnit("test");
        var objectResult = result.Result as ObjectResult;

        var def = objectResult?.Value as IEnumerable<DefinitionDTO>;

        Assert.AreEqual(200, objectResult?.StatusCode);
        Assert.AreEqual(0, def?.Count());
    }

    [TestMethod]
    public async Task GetDefinitionsByUnit_EmptyUnit()
    {
        var definitions = _fixture.CreateMany<Definition>(5).ToList();
        Unit unit = null;

        _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
        _definitionRepoMock.Setup(repo => repo.GetDefinitionsByUnit(It.IsAny<int>())).ReturnsAsync(definitions);

        _controller = new DefinitionController(_definitionRepoMock.Object, _unitRepoMock.Object);

        var result = await _controller.GetDefinitionsByUnit("test");
        var objectResult = result.Result as StatusCodeResult;

        Assert.AreEqual(404, objectResult?.StatusCode);
    }

    // Posts
    [TestMethod]
    public async Task AddDefinition_Correct()
    {
        Unit unit = _fixture.Create<Unit>();

        Definition definition = _fixture.Create<Definition>();
        definition.unitId = unit.Id;
        DefinitionDTO definitionDTO = new DefinitionDTO()
        {
            name = definition.Name,
            part1 = definition.Part1,
            part2 = definition.Part2,
            ID = definition.Id,
            type = definition.Type,
            UnitName = unit.name
        };

        _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
        _definitionRepoMock.Setup(repo => repo.AddDefinition(It.IsAny<Definition>())).ReturnsAsync(definition);
        _controller = new DefinitionController(_definitionRepoMock.Object, _unitRepoMock.Object);

        var result = await _controller.AddDefinition(definitionDTO);
        var objectResult = result.Result as ObjectResult;

        var def = objectResult?.Value as DefinitionDTO;

        Assert.AreEqual(201, objectResult?.StatusCode);
        Assert.AreEqual(definition.Name, def?.name);
        Assert.AreEqual(definition.Part1, def?.part1);
        Assert.AreEqual(definition.Part2, def?.part2);
        Assert.AreEqual(unit.name, def?.UnitName);
    }

    [TestMethod]
    public async Task AddDefinition_Exeption()
    {
       
        DefinitionDTO definition = _fixture.Create<DefinitionDTO>();

        _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ThrowsAsync(new Exception("Test exception"));
        _definitionRepoMock.Setup(repo => repo.AddDefinition(It.IsAny<Definition>())).ThrowsAsync(new Exception("Test exception"));

        _controller = new DefinitionController(_definitionRepoMock.Object, _unitRepoMock.Object);

        var result = await _controller.AddDefinition(definition);
        var objectResult = result.Result as ObjectResult;

        Assert.AreEqual(400, objectResult?.StatusCode);
    }

    [TestMethod]
    public async Task AddDefinition_Empty()
    {

        DefinitionDTO definitionDTO = _fixture.Create<DefinitionDTO>();
        Definition definition = _fixture.Create<Definition>();
        Unit unit = null;

        _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
        _definitionRepoMock.Setup(repo => repo.AddDefinition(It.IsAny<Definition>())).ReturnsAsync(definition);

        _controller = new DefinitionController(_definitionRepoMock.Object, _unitRepoMock.Object);

        var result = await _controller.AddDefinition(definitionDTO);
        var objectResult = result.Result as StatusCodeResult;

        Assert.AreEqual(404, objectResult?.StatusCode);
    }



    [TestMethod]
    public async Task UpdatDefinition_Correct()
    {
        Unit unit = _fixture.Create<Unit>();

        Definition definition = _fixture.Create<Definition>();
        definition.unitId = unit.Id;
        DefinitionDTO definitionDTO = new DefinitionDTO()
        {
            name = definition.Name,
            part1 = definition.Part1,
            part2 = definition.Part2,
            ID = definition.Id,
            type = definition.Type,
            UnitName = unit.name
        };

        _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
        _definitionRepoMock.Setup(repo => repo.EditDefinition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(true);
        _controller = new DefinitionController(_definitionRepoMock.Object, _unitRepoMock.Object);

        var result = await _controller.UpdatDefinition(definitionDTO);
        var objectResult = result as StatusCodeResult;


        Assert.AreEqual(200, objectResult?.StatusCode);       
    }

    [TestMethod]
    public async Task UpdatDefinition_Exeption()
    {

        DefinitionDTO definition = _fixture.Create<DefinitionDTO>();

        _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ThrowsAsync(new Exception("Test exception"));
        _definitionRepoMock.Setup(repo => repo.EditDefinition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ThrowsAsync(new Exception("Test exception"));

        _controller = new DefinitionController(_definitionRepoMock.Object, _unitRepoMock.Object);

        var result = await _controller.UpdatDefinition(definition);
        var objectResult = result as ObjectResult;

        Assert.AreEqual(400, objectResult?.StatusCode);
    }

    [TestMethod]
    public async Task UpdatDefinition_Empty()
    {

        DefinitionDTO definitionDTO = _fixture.Create<DefinitionDTO>();
       // Definition definition = _fixture.Create<Definition>();
        Unit unit = _fixture.Create<Unit>();

        _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
        _definitionRepoMock.Setup(repo => repo.EditDefinition(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(false);

        _controller = new DefinitionController(_definitionRepoMock.Object, _unitRepoMock.Object);

        var result = await _controller.UpdatDefinition(definitionDTO);
        var objectResult = result as StatusCodeResult;

        Assert.AreEqual(404, objectResult?.StatusCode);
    }
}
