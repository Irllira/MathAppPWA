using API.Enteties;
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
    public class IncorrectControllerTest
    {
        private Mock<IDefinitionRepo> _definitionRepoMock;
        private Mock<IIncorrectDefinitionRepo> _incorrectRepoMock;

        private Fixture _fixture;
        private IncorrectController _controller;


        public IncorrectControllerTest()
        {
            _definitionRepoMock = new Mock<IDefinitionRepo>();
            _incorrectRepoMock = new Mock<IIncorrectDefinitionRepo>();
            _fixture = new Fixture();
        }

        [TestMethod]
        public async Task GetAllIncorrect_CorrectAsync()
        {
            var incorrects = _fixture.CreateMany<IncorrectDefinition>(5).ToList();

            _incorrectRepoMock.Setup(repo => repo.GetAllIncorrect()).ReturnsAsync(incorrects);
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.GetAllIncorrect();
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as IEnumerable<IncorrectDTO>;

            Assert.AreEqual(200, objectResult?.StatusCode);
            Assert.AreEqual(5, def?.Count());
        }      

        [TestMethod]
        public async Task GetAllIncorrect_Exception()
        {
            //var definitionDTO = _fixture.CreateMany<IncorrectDefinition>(5).ToList();

            _incorrectRepoMock.Setup(repo => repo.GetAllIncorrect()).ThrowsAsync(new Exception("Test exception"));
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.GetAllIncorrect();
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(400, objectResult?.StatusCode);
        }

        [TestMethod]
        public async Task GetAllIncorrect_Empty()
        {
            IEnumerable<IncorrectDefinition> incorrects = null;

            _incorrectRepoMock.Setup(repo => repo.GetAllIncorrect()).ReturnsAsync(incorrects);
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.GetAllIncorrect();
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }


        [TestMethod]
        public async Task GetByID_CorrectAsync()
        {
            int a = 0;
            var incorrect = _fixture.Create<IncorrectDefinition>;// (5).ToList();

            _incorrectRepoMock.Setup(repo => repo.GetIncorrectByID(a)).ReturnsAsync(incorrect);
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.GetByID(a);
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as IEnumerable<IncorrectDTO>;

            Assert.AreEqual(200, objectResult?.StatusCode);
           // Assert.AreEqual(5, definition?.Count());
        }

        [TestMethod]
        public async Task GetByID_Exception()
        {
            int a = 0;
            _incorrectRepoMock.Setup(repo => repo.GetIncorrectByID(a)).ThrowsAsync(new Exception("Test exception"));
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.GetByID(a);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(400, objectResult?.StatusCode);
        }

        [TestMethod]
        public async Task GetByID_Empty()
        {
            int a = 0;
            IncorrectDefinition incorrect = null;

            _incorrectRepoMock.Setup(repo => repo.GetIncorrectByID(a)).ReturnsAsync(incorrect);
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.GetByID(a);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }


        [TestMethod]
        public async Task GetByContent_CorrectAsync()
        {
            string a = "Test";

            var incorrect = _fixture.Create<IncorrectDefinition>;// (5).ToList();

            _incorrectRepoMock.Setup(repo => repo.GetIncorrectByContent(a)).ReturnsAsync(incorrect);
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.GetByContent(a);
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as IEnumerable<IncorrectDTO>;

            Assert.AreEqual(200, objectResult?.StatusCode);
            // Assert.AreEqual(5, definition?.Count());
        }

        [TestMethod]
        public async Task GetByContent_Exception()
        {
            string a = "Test";

            _incorrectRepoMock.Setup(repo => repo.GetIncorrectByContent(a)).ThrowsAsync(new Exception("Test exception"));
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.GetByContent(a);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(400, objectResult?.StatusCode);
        }

        [TestMethod]
        public async Task GetByContent_Empty()
        {
            string a = "Test";
            IncorrectDefinition incorrect = null;

            _incorrectRepoMock.Setup(repo => repo.GetIncorrectByContent(a)).ReturnsAsync(incorrect);
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.GetByContent(a);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }

        [TestMethod]
        public async Task GetByDefinition_CorrectAsync()
        {
            int a = 0;
            var incorrects = _fixture.CreateMany<IncorrectDefinition>(5).ToList();

            _incorrectRepoMock.Setup(repo => repo.GetIncorrectsByDefinition(a)).ReturnsAsync(incorrects);
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.GetByDefinition(a);
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as IEnumerable<IncorrectDTO>;

            Assert.AreEqual(200, objectResult?.StatusCode);
            Assert.AreEqual(5, def?.Count());
        }

        [TestMethod]
        public async Task GetByDefinition_Exception()
        {
            int a = 0;
            _incorrectRepoMock.Setup(repo => repo.GetIncorrectsByDefinition(a)).ThrowsAsync(new Exception("Test exception"));
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.GetByDefinition(a);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(400, objectResult?.StatusCode);
        }

        [TestMethod]
        public async Task GetByDefinition_Empty()
        {
            int a = 0;
            IEnumerable<IncorrectDefinition> incorrects = null;

            _incorrectRepoMock.Setup(repo => repo.GetIncorrectsByDefinition(a)).ReturnsAsync(incorrects);
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.GetByDefinition(a);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }


        [TestMethod]
        public async Task GetPairs_CorrectAsync()
        {
            var pairs = _fixture.CreateMany<DefIncPair>(5).ToList();

            _incorrectRepoMock.Setup(repo => repo.GetAllPairs()).ReturnsAsync(pairs);
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.GetPairs();
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as IEnumerable<DefIncPairDTO>;

            Assert.AreEqual(200, objectResult?.StatusCode);
            Assert.AreEqual(5, def?.Count());
        }

        [TestMethod]
        public async Task GetPairs_Exception()
        {
           
            _incorrectRepoMock.Setup(repo => repo.GetAllPairs()).ThrowsAsync(new Exception("Test exception"));
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.GetPairs();
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(400, objectResult?.StatusCode);
        }

        [TestMethod]
        public async Task GetPairs_Empty()
        {
            int a = 0;
            IEnumerable<DefIncPair> incorrects = null;

            _incorrectRepoMock.Setup(repo => repo.GetAllPairs()).ReturnsAsync(incorrects);
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.GetPairs();
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }

        [TestMethod]
        public async Task GetPairsByDef_CorrectAsync()
        {
            int a = 0;
            var pairs = _fixture.CreateMany<DefIncPair>(5).ToList();

            _incorrectRepoMock.Setup(repo => repo.GetPairsByDefinition(a)).ReturnsAsync(pairs);
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.GetPairsByDef(a);
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as IEnumerable<DefIncPairDTO>;

            Assert.AreEqual(200, objectResult?.StatusCode);
            Assert.AreEqual(5, def?.Count());
        }

        [TestMethod]
        public async Task GetPairsByDef_Exception()
        {
            int a = 0;
            _incorrectRepoMock.Setup(repo => repo.GetPairsByDefinition(a)).ThrowsAsync(new Exception("Test exception"));
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.GetPairsByDef(a);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(400, objectResult?.StatusCode);
        }

        [TestMethod]
        public async Task GetPairsByDef_Empty()
        {
            int a = 0;
            IEnumerable<DefIncPair> incorrects = null;

            _incorrectRepoMock.Setup(repo => repo.GetPairsByDefinition(a)).ReturnsAsync(incorrects);
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.GetPairsByDef(a);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }

// Posts 
        [TestMethod]    
        public async Task AddIncorrect_Correct()
        {
            var incorrectDTO = _fixture.Create<IncorrectDTO>();
            var incorrenct = new IncorrectDefinition() { Content = incorrectDTO.content, Id = incorrectDTO.Id };

            _incorrectRepoMock.Setup(repo => repo.AddIncorrect(It.IsAny<IncorrectDefinition>())).ReturnsAsync(incorrenct);
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.AddIncorrect(incorrectDTO);
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as IncorrectDTO;

            Assert.AreEqual(201, objectResult?.StatusCode);
            Assert.AreEqual(incorrectDTO.Id, def?.Id);
            Assert.AreEqual(incorrectDTO.content, def?.content);
        }

        [TestMethod]
        public async Task AddIncorrect_Exception()
        {
            var incorrectDTO = _fixture.Create<IncorrectDTO>();

            _incorrectRepoMock.Setup(repo => repo.AddIncorrect(It.IsAny<IncorrectDefinition>())).ThrowsAsync(new Exception("Test exception"));
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.AddIncorrect(incorrectDTO);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(400, objectResult?.StatusCode);           
        }

        [TestMethod]
        public async Task AddIncorrect_Empty()
        {
            var incorrectDTO = _fixture.Create<IncorrectDTO>();
            IncorrectDefinition incorrenct = null;

            _incorrectRepoMock.Setup(repo => repo.AddIncorrect(It.IsAny<IncorrectDefinition>())).ReturnsAsync(incorrenct);
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.AddIncorrect(incorrectDTO);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }

        [TestMethod]
        public async Task AddPair_Correct()
        {
            var definitionDTO = _fixture.Create<DefIncPairDTO>();
            var definition = new DefIncPair(){ DefinitionId = definitionDTO.DefinitionID, IncorrectDefinitionId = definitionDTO.IncorrectID};

            _incorrectRepoMock.Setup(repo => repo.AddPair(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(definition);
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.AddPair(definitionDTO);
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as DefIncPairDTO;

            Assert.AreEqual(201, objectResult?.StatusCode);
            Assert.AreEqual(definitionDTO.DefinitionID, def?.DefinitionID);
            Assert.AreEqual(definitionDTO.IncorrectID, def?.IncorrectID);
        }

        [TestMethod]
        public async Task AddPair_Exception()
        {
            var pairDTO = _fixture.Create<DefIncPairDTO>();

            _incorrectRepoMock.Setup(repo => repo.AddPair(It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception("Test exception"));
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.AddPair(pairDTO);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(400, objectResult?.StatusCode);
        }

        [TestMethod]
        public async Task AddPair_Empty()
        {
            var definitionDTO = _fixture.Create<DefIncPairDTO>();
            DefIncPair definition = null;

            _incorrectRepoMock.Setup(repo => repo.AddPair(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(definition);
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.AddPair(definitionDTO);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }

        [TestMethod]
        public async Task DeletePair_Correct()
        {          
            _incorrectRepoMock.Setup(repo => repo.DeletePair(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.DeletePair(0,0);
            var objectResult = result as StatusCodeResult;

            Assert.AreEqual(200, objectResult?.StatusCode);
        }

        [TestMethod]
        public async Task DeletePair_Exception()
        {           
            _incorrectRepoMock.Setup(repo => repo.DeletePair(It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception("Test exception"));
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.DeletePair(0,0);
            var objectResult = result as StatusCodeResult;

            Assert.AreEqual(400, objectResult?.StatusCode);
        }

        [TestMethod]
        public async Task DeletePair_Empty()
        {
            var definitionDTO = _fixture.Create<DefIncPairDTO>();
            DefIncPair definition = null;

            _incorrectRepoMock.Setup(repo => repo.DeletePair(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(false);
            _controller = new IncorrectController(_definitionRepoMock.Object, _incorrectRepoMock.Object);

            var result = await _controller.DeletePair(0,0);
            var objectResult = result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }
    }
}