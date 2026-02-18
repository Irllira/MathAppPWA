using AutoFixture;
using DTO.DTOs;
using MathApp.Backend.API.Controllers;
using MathApp.Backend.API.Interfaces;
using MathApp.Backend.Data.Enteties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathApp.Testing.API.Controllers.Tests
{
    [TestClass()]
    public class UnitControllerTest
    {
        private Mock<IUnitRepo> _unitRepoMock;
        private Mock<IEducationLevelRepo> _edLevelRepoMock;

        private Fixture _fixture;
        private UnitController _controller;

        public UnitControllerTest()
        {
            _unitRepoMock = new Mock<IUnitRepo>();
            _edLevelRepoMock = new Mock<IEducationLevelRepo>();
            _fixture = new Fixture();
        }

        [TestMethod()]
        public async Task GetUnits_Correct()
        {
            var units = _fixture.CreateMany<Unit>(5).ToList();
            var edLvl = _fixture.Create<EducationLevel>();

            _unitRepoMock.Setup(repo => repo.GetAllUnit()).ReturnsAsync(units);
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelByID(It.IsAny<int>())).ReturnsAsync(edLvl);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.GetUnits();
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as IEnumerable<UnitDTO>;

            Assert.AreEqual(200, objectResult?.StatusCode);
            Assert.AreEqual(5, def?.Count());
        }

        [TestMethod()]
        public async Task GetUnits_Exception()
        {
            var edLvl = _fixture.Create<EducationLevel>();

            _unitRepoMock.Setup(repo => repo.GetAllUnit()).ThrowsAsync(new Exception("Test exception"));
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelByID(It.IsAny<int>())).ReturnsAsync(edLvl);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.GetUnits();
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(400, objectResult?.StatusCode);
        }

        [TestMethod()]
        public async Task GetUnits_EmptyUnits()
        {
           IEnumerable<Unit> units = null;
            var edLvl = _fixture.Create<EducationLevel>();

            _unitRepoMock.Setup(repo => repo.GetAllUnit()).ReturnsAsync(units);
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelByID(It.IsAny<int>())).ReturnsAsync(edLvl);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.GetUnits();
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }

        [TestMethod()]
        public async Task GetUnits_EmptyEdLvlAll()
        {
            var units = _fixture.CreateMany<Unit>(5).ToList();
          // var edLvl = _fixture.Create<EducationLevel>();
            EducationLevel edLvl = null;

            _unitRepoMock.Setup(repo => repo.GetAllUnit()).ReturnsAsync(units);
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelByID(It.IsAny<int>())).ReturnsAsync(edLvl);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.GetUnits();
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as IEnumerable<UnitDTO>;

            Assert.AreEqual(200, objectResult?.StatusCode);
            Assert.AreEqual(0, def?.Count());
        }

        [TestMethod()]
        public async Task GetUnits_EmptyEdLvlOne()
        {
            var units = _fixture.CreateMany<Unit>(3).ToList();
            var edLvl = _fixture.Create<EducationLevel>();
            EducationLevel edLvlEmpty = null;

            _unitRepoMock.Setup(repo => repo.GetAllUnit()).ReturnsAsync(units);
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelByID(units[0].educationLevelId)).ReturnsAsync(edLvl);
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelByID(units[1].educationLevelId)).ReturnsAsync(edLvlEmpty);
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelByID(units[2].educationLevelId)).ReturnsAsync(edLvl);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.GetUnits();
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as IEnumerable<UnitDTO>;

            Assert.AreEqual(200, objectResult?.StatusCode);
            Assert.AreEqual(2, def?.Count());
        }

        [TestMethod()]
        public async Task GetUnitsByEdLvls_Correct()
        {
            var units = _fixture.CreateMany<Unit>(5).ToList();
            var edLvl = _fixture.Create<EducationLevel>();

            _unitRepoMock.Setup(repo => repo.GetUnitsbyEdLevel(It.IsAny<int>())).ReturnsAsync(units);
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelsbyName(It.IsAny<string>())).ReturnsAsync(edLvl);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.GetUnitsByEdLvls("Test");
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as IEnumerable<UnitDTO>;

            Assert.AreEqual(200, objectResult?.StatusCode);
            Assert.AreEqual(5, def?.Count());
        }

        [TestMethod()]
        public async Task GetUnitsByEdLvls_Exception()
        {
            var edLvl = _fixture.Create<EducationLevel>();

            _unitRepoMock.Setup(repo => repo.GetUnitsbyEdLevel(It.IsAny<int>())).ThrowsAsync(new Exception("Test exception"));
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelsbyName(It.IsAny<string>())).ReturnsAsync(edLvl);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.GetUnitsByEdLvls("Test");
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(400, objectResult?.StatusCode);
        }


        [TestMethod()]
        public async Task GetUnitsByEdLvls_EmptyEdLvl()
        {
            var units = _fixture.CreateMany<Unit>(5).ToList();
            EducationLevel edLvl = null;

            _unitRepoMock.Setup(repo => repo.GetUnitsbyEdLevel(It.IsAny<int>())).ReturnsAsync(units);
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelsbyName(It.IsAny<string>())).ReturnsAsync(edLvl);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.GetUnitsByEdLvls("Test");
            var objectResult = result.Result as StatusCodeResult;


            Assert.AreEqual(404, objectResult?.StatusCode);
        }

         [TestMethod()]
        public async Task GetUnitsByEdLvls_EmptyUnit()
        {
            IEnumerable<Unit> units = null;
            EducationLevel edLvl = _fixture.Create<EducationLevel>(); ;

            _unitRepoMock.Setup(repo => repo.GetUnitsbyEdLevel(It.IsAny<int>())).ReturnsAsync(units);
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelsbyName(It.IsAny<string>())).ReturnsAsync(edLvl);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.GetUnitsByEdLvls("Test");
            var objectResult = result.Result as StatusCodeResult;


            Assert.AreEqual(404, objectResult?.StatusCode);
        }


        [TestMethod()]
        public async Task GetUnitByName_Correct()
        {
            var units = _fixture.Create<Unit>;
            var edLvl = _fixture.Create<EducationLevel>();

            _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(units);
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelName(It.IsAny<int>())).ReturnsAsync(edLvl.name);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.GetUnitByName("Test");
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as UnitDTO;

            Assert.AreEqual(200, objectResult?.StatusCode);
            Assert.AreEqual(edLvl.name, def?.educationLevel);
        }

        [TestMethod()]
        public async Task GetUnitByName_Exception()
        {
            var edLvl = _fixture.Create<EducationLevel>();

            _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ThrowsAsync(new Exception("Test exception"));
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelName(It.IsAny<int>())).ReturnsAsync(edLvl.name);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.GetUnitByName("Test");
            var objectResult = result.Result as StatusCodeResult;


            Assert.AreEqual(400, objectResult?.StatusCode);
        }
        [TestMethod()]
        public async Task GetUnitByName_EmptyUnit()
        {
            Unit units = null;
            var edLvl = _fixture.Create<EducationLevel>();

            _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(units);
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelName(It.IsAny<int>())).ReturnsAsync(edLvl.name);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.GetUnitByName("Test");
            var objectResult = result.Result as StatusCodeResult;


            Assert.AreEqual(404, objectResult?.StatusCode);
        }
        [TestMethod()]
        public async Task GetUnitByName_EmptyEdLvl()
        {
            Unit units = _fixture.Create<Unit>();
            string edLvl = null;

            _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(units);
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelName(It.IsAny<int>())).ReturnsAsync(edLvl);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.GetUnitByName("Test");
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }

        [TestMethod()]
        public async Task AddUnit_Exception()
        {
            Unit unit = _fixture.Create<Unit>();
            EducationLevel edLvl = _fixture.Create<EducationLevel>();
            var unitDTO = new UnitDTO() { educationLevel = edLvl.name, name = unit.name, ID = unit.Id, description = unit.description };

            _unitRepoMock.Setup(repo => repo.AddUnit(It.IsAny<Unit>())).ThrowsAsync(new Exception("Test exception"));
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelsbyName(It.IsAny<string>())).ReturnsAsync(edLvl);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.AddUnit(unitDTO);
            var objectResult = result.Result as StatusCodeResult;


            Assert.AreEqual(400, objectResult?.StatusCode);
        }

        [TestMethod()]
        public async Task AddUnit_Correct()
        {
            Unit unit = _fixture.Create<Unit>();
            EducationLevel edLvl = _fixture.Create<EducationLevel>();
            var unitDTO = new UnitDTO() { educationLevel = edLvl.name, name = unit.name, ID = unit.Id, description = unit.description };

            _unitRepoMock.Setup(repo => repo.AddUnit(It.IsAny<Unit>())).ReturnsAsync(unit);
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelsbyName(It.IsAny<string>())).ReturnsAsync(edLvl);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.AddUnit(unitDTO);
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as UnitDTO;

            Assert.AreEqual(201, objectResult?.StatusCode);
            Assert.AreEqual(edLvl.name, def?.educationLevel);
        }

        [TestMethod()]
        public async Task AddUnit_EmptyUnit()
        {
            Unit unit = null;
            EducationLevel edLvl = _fixture.Create<EducationLevel>();
            var unitDTO = _fixture.Create<UnitDTO>();

            _unitRepoMock.Setup(repo => repo.AddUnit(It.IsAny<Unit>())).ReturnsAsync(unit);
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelsbyName(It.IsAny<string>())).ReturnsAsync(edLvl);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.AddUnit(unitDTO);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }
        [TestMethod()]
        public async Task AddUnit_EmptyEdLvl()
        {
            Unit unit = _fixture.Create<Unit>();
            EducationLevel edLvl = null;
            var unitDTO = new UnitDTO() { educationLevel ="edLvl", name = unit.name, ID = unit.Id, description = unit.description };
            
            _unitRepoMock.Setup(repo => repo.AddUnit(It.IsAny<Unit>())).ReturnsAsync(unit);
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelsbyName(It.IsAny<string>())).ReturnsAsync(edLvl);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.AddUnit(unitDTO);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }


        [TestMethod()]
        public async Task EditUnit_Correct()
        {
            Unit unit = _fixture.Create<Unit>();
            EducationLevel edLvl = _fixture.Create<EducationLevel>();
            var unitDTO = new UnitDTO() { educationLevel = edLvl.name, name = unit.name, ID = unit.Id, description = unit.description };

            _unitRepoMock.Setup(repo => repo.EditUnit(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(unit);
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelsbyName(It.IsAny<string>())).ReturnsAsync(edLvl);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.EditUnit(unitDTO);
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as UnitDTO;

            Assert.AreEqual(201, objectResult?.StatusCode);
            Assert.AreEqual(edLvl.name, def?.educationLevel);
        }

        [TestMethod()]
        public async Task EditUnit_Exception()
        {
            Unit unit = _fixture.Create<Unit>();
            EducationLevel edLvl = _fixture.Create<EducationLevel>();
            var unitDTO = new UnitDTO() { educationLevel = edLvl.name, name = unit.name, ID = unit.Id, description = unit.description };

            _unitRepoMock.Setup(repo => repo.EditUnit(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(unit);
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelsbyName(It.IsAny<string>())).ThrowsAsync(new Exception("Test exception"));

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.EditUnit(unitDTO);
            var objectResult = result.Result as StatusCodeResult;


            Assert.AreEqual(400, objectResult?.StatusCode);
        }


        [TestMethod()]
        public async Task EditUnit_EmptyUnit()
        {
            Unit unit = null;
            EducationLevel edLvl = _fixture.Create<EducationLevel>();
            var unitDTO = _fixture.Create<UnitDTO>();

            _unitRepoMock.Setup(repo => repo.EditUnit(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(unit);
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelsbyName(It.IsAny<string>())).ReturnsAsync(edLvl);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.EditUnit(unitDTO);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }


        [TestMethod()]
        public async Task EditUnit_EmptyEdLvl()
        {
            Unit unit = _fixture.Create<Unit>();
            EducationLevel edLvl = null;
            var unitDTO = new UnitDTO() { educationLevel = "ed", name = unit.name, ID = unit.Id, description = unit.description };

            _unitRepoMock.Setup(repo => repo.EditUnit(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(unit);
            _edLevelRepoMock.Setup(repo => repo.GetEducationLevelsbyName(It.IsAny<string>())).ReturnsAsync(edLvl);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.EditUnit(unitDTO);
            var objectResult = result.Result as StatusCodeResult;


            Assert.AreEqual(404, objectResult?.StatusCode);
        }
        [TestMethod()]
        public async Task DeleteUnit_Correct()
        {
            _unitRepoMock.Setup(repo => repo.RemoveUnitByName(It.IsAny<string>())).ReturnsAsync(true);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.DeleteUnit("Test");
            var objectResult = result as StatusCodeResult;

            Assert.AreEqual(200, objectResult?.StatusCode);
        }
        [TestMethod()]
        public async Task DeleteUnit_Exception()
        {
            _unitRepoMock.Setup(repo => repo.RemoveUnitByName(It.IsAny<string>())).ThrowsAsync(new Exception("Test exception"));

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.DeleteUnit("Test");
            var objectResult = result as StatusCodeResult;

            Assert.AreEqual(400, objectResult?.StatusCode);
        }
        [TestMethod()]
        public async Task DeleteUnit_Empty()
        {
            _unitRepoMock.Setup(repo => repo.RemoveUnitByName(It.IsAny<string>())).ReturnsAsync(false);

            _controller = new UnitController(_unitRepoMock.Object, _edLevelRepoMock.Object);

            var result = await _controller.DeleteUnit("Test");
            var objectResult = result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }
    }
}