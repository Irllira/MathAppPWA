using AutoFixture;
using DTO.DTOs;
using MathApp.Backend.API.Controllers;
using MathApp.Backend.API.Interfaces;
using MathApp.Backend.Data.Enteties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    public class PagesControllerTest
    {
        private Mock<IPagesRepo> _pagesRepoMock;
        private Fixture _fixture;
        private PagesController _controller;

        public PagesControllerTest()
        {
            _pagesRepoMock = new Mock<IPagesRepo>();
            _fixture = new Fixture();
        }

        [TestMethod()]
        public async Task GetPages_Correct()
        {
            var pages = _fixture.CreateMany<Pages>(5).ToList();

            _pagesRepoMock.Setup(repo => repo.GetAllPages()).ReturnsAsync(pages);
            _controller = new PagesController(_pagesRepoMock.Object);

            var result = await _controller.GetPages();
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as IEnumerable<PagesDTO>;

            Assert.AreEqual(200, objectResult?.StatusCode);
            Assert.AreEqual(5, def?.Count());
        }

        [TestMethod()]
        public async Task GetPages_Exception()
        {
            _pagesRepoMock.Setup(repo => repo.GetAllPages()).ThrowsAsync(new Exception("Test exception"));
            _controller = new PagesController(_pagesRepoMock.Object);

            var result = await _controller.GetPages();
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(400, objectResult?.StatusCode);
        }

        [TestMethod()]
        public async Task GetPages_Empty()
        {
            IEnumerable<Pages> pages = null;

            _pagesRepoMock.Setup(repo => repo.GetAllPages()).ReturnsAsync(pages);
            _controller = new PagesController(_pagesRepoMock.Object);

            var result = await _controller.GetPages();
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }
        [TestMethod()]
        public async Task GetPagesByUnitID_Correct()
        {
            var pages = _fixture.CreateMany<Pages>(5).ToList();

            _pagesRepoMock.Setup(repo => repo.GetPagesByUnit(It.IsAny<int>())).ReturnsAsync(pages);
            _controller = new PagesController(_pagesRepoMock.Object);

            var result = await _controller.GetPagesByUnitID(0);
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as IEnumerable<PagesDTO>;

            Assert.AreEqual(200, objectResult?.StatusCode);
            Assert.AreEqual(5, def?.Count());
        }

        [TestMethod()]
        public async Task GetPagesByUnitID_Exception()
        {
            _pagesRepoMock.Setup(repo => repo.GetPagesByUnit(It.IsAny<int>())).ThrowsAsync(new Exception("Test exception"));
            _controller = new PagesController(_pagesRepoMock.Object);

            var result = await _controller.GetPagesByUnitID(0);
            var objectResult = result.Result as StatusCodeResult;


            Assert.AreEqual(400, objectResult?.StatusCode);
        }

        [TestMethod()]
        public async Task GetPagesByUnitID_Empty()
        {
            IEnumerable<Pages> pages = null;

            _pagesRepoMock.Setup(repo => repo.GetPagesByUnit(It.IsAny<int>())).ReturnsAsync(pages);
            _controller = new PagesController(_pagesRepoMock.Object);

            var result = await _controller.GetPagesByUnitID(0);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }

        [TestMethod()]
        public async Task AddPage_Correct()
        {
            var pageDTO = _fixture.Create<PagesDTO>();
            var page = new Pages() { Link = pageDTO.link, Name = pageDTO.Name, UnitID = pageDTO.UnitID, Id = pageDTO.Id };

            _pagesRepoMock.Setup(repo => repo.AddPage(It.IsAny<Pages>())).ReturnsAsync(page);
            _controller = new PagesController(_pagesRepoMock.Object);

            var result = await _controller.AddPage(pageDTO);
            var objectResult = result.Result as ObjectResult;

            var pg = objectResult?.Value as PagesDTO;

            Assert.AreEqual(201, objectResult?.StatusCode);
            Assert.AreEqual(page.Name, pg?.Name);
            Assert.AreEqual(page.Link, pg?.link);
        }

        [TestMethod()]
        public async Task AddPage_Exception()
        {
            var pageDTO = _fixture.Create<PagesDTO>();
            var page = new Pages() { Link = pageDTO.link, Name = pageDTO.Name, UnitID = pageDTO.UnitID, Id = pageDTO.Id };

            _pagesRepoMock.Setup(repo => repo.AddPage(It.IsAny<Pages>())).ThrowsAsync(new Exception("Test exception"));
            _controller = new PagesController(_pagesRepoMock.Object);

            var result = await _controller.AddPage(pageDTO);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(400, objectResult?.StatusCode);           
        }

        [TestMethod()]
        public async Task AddPage_Empty()
        {
            var pageDTO = _fixture.Create<PagesDTO>();
            Pages page = null;

            _pagesRepoMock.Setup(repo => repo.AddPage(It.IsAny<Pages>())).ReturnsAsync(page);
            _controller = new PagesController(_pagesRepoMock.Object);

            var result = await _controller.AddPage(pageDTO);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);           
        }
        [TestMethod()]
        public async Task UpdatePage_Correct()
        {
            var pageDTO = _fixture.Create<PagesDTO>();
            var page = new Pages() { Link = pageDTO.link, Name = pageDTO.Name, UnitID = pageDTO.UnitID, Id = pageDTO.Id };

            _pagesRepoMock.Setup(repo => repo.UpdatePage(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(page);
            _controller = new PagesController(_pagesRepoMock.Object);

            var result = await _controller.UpdatePage(pageDTO);
            var objectResult = result.Result as ObjectResult;

            var pg = objectResult?.Value as PagesDTO;

            Assert.AreEqual(201, objectResult?.StatusCode);
            Assert.AreEqual(page.Name, pg?.Name);
            Assert.AreEqual(page.Link, pg?.link);
        }
        [TestMethod()]

        public async Task UpdatePage_Exception()
        {
            var pageDTO = _fixture.Create<PagesDTO>();
            var page = new Pages() { Link = pageDTO.link, Name = pageDTO.Name, UnitID = pageDTO.UnitID, Id = pageDTO.Id };

            _pagesRepoMock.Setup(repo => repo.UpdatePage(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ThrowsAsync(new Exception("Test exception"));
            _controller = new PagesController(_pagesRepoMock.Object);

            var result = await _controller.UpdatePage(pageDTO);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(400, objectResult?.StatusCode);
        }
        [TestMethod()]

        public async Task UpdatePage_Empty()
        {
            var pageDTO = _fixture.Create<PagesDTO>();
            Pages page = null;// new Pages() { Link = pageDTO.link, Name = pageDTO.Name, UnitID = pageDTO.UnitID, Id = pageDTO.Id };

            _pagesRepoMock.Setup(repo => repo.UpdatePage(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(page);
            _controller = new PagesController(_pagesRepoMock.Object);

            var result = await _controller.UpdatePage(pageDTO);
            var objectResult = result.Result as StatusCodeResult;


            Assert.AreEqual(404, objectResult?.StatusCode);
        }


        [TestMethod()]
        public async Task DeletePage_Correct()
        {
            _pagesRepoMock.Setup(repo => repo.DeletePage(It.IsAny<int>())).ReturnsAsync(true);
            _controller = new PagesController(_pagesRepoMock.Object);

            var result = await _controller.DeletePage(0);
            var objectResult = result as StatusCodeResult;

            Assert.AreEqual(200, objectResult?.StatusCode);
        }

        [TestMethod()]
        public async Task DeletePage_Exception()
        {
            _pagesRepoMock.Setup(repo => repo.DeletePage(It.IsAny<int>())).ThrowsAsync(new Exception("Test exception")); ;
            _controller = new PagesController(_pagesRepoMock.Object);

            var result = await _controller.DeletePage(0);
            var objectResult = result as StatusCodeResult;

            Assert.AreEqual(400, objectResult?.StatusCode);
        }

        [TestMethod()]
        public async Task DeletePage_Empty()
        {           
            _pagesRepoMock.Setup(repo => repo.DeletePage(It.IsAny<int>())).ReturnsAsync(false);
            _controller = new PagesController(_pagesRepoMock.Object);

            var result = await _controller.DeletePage(0);
            var objectResult = result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }

    }
}