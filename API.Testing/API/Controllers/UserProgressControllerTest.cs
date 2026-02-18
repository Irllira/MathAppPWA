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
    public class UserProgressControllerTest
    {
        private Mock<IUserProgressRepo> _progresRepoMock;
        private Mock<IAccountRepo> _accountRepoMock;
        private Mock<IUnitRepo> _unitRepoMock;

        private Fixture _fixture;
        private UserProgressController _controller;
        public UserProgressControllerTest()
        {
            _progresRepoMock = new Mock<IUserProgressRepo>();
            _unitRepoMock = new Mock<IUnitRepo>();
            _accountRepoMock = new Mock<IAccountRepo>();
            _fixture = new Fixture();
        }

        [TestMethod()]
        public async Task GetAllUserProgress_Correct()
        {
            var progress = _fixture.CreateMany<UserProgress>(5).ToList();
            var unit = _fixture.Create<Unit>();
            var account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.GetUserProgress()).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByID(It.IsAny<int>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountById(It.IsAny<int>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.GetAllUserProgress();
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as IEnumerable<UserProgressDTO>;

            Assert.AreEqual(200, objectResult?.StatusCode);
            Assert.AreEqual(5, def?.Count());
        }

        [TestMethod()]

        public async Task GetAllUserProgress_Exception()
        {
           // var progress = _fixture.CreateMany<UserProgress>(5).ToList();
            var unit = _fixture.Create<Unit>();
            var account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.GetUserProgress()).ThrowsAsync(new Exception("Test exception"));
            _unitRepoMock.Setup(repo => repo.GetUnitByID(It.IsAny<int>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountById(It.IsAny<int>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.GetAllUserProgress();
            var objectResult = result.Result as ObjectResult;

            //var prg = objectResult?.Value as IEnumerable<UserProgressDTO>;

            Assert.AreEqual(400, objectResult?.StatusCode);
          //  Assert.AreEqual(5, prg?.Count());
        }

        [TestMethod()]
        public async Task GetAllUserProgress_EmptyProgress()
        {
            IEnumerable<UserProgress> progress = null;
            var unit = _fixture.Create<Unit>();
            var account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.GetUserProgress()).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByID(It.IsAny<int>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountById(It.IsAny<int>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.GetAllUserProgress();
            var objectResult = result.Result as StatusCodeResult;

            //var prg = objectResult?.Value as IEnumerable<UserProgressDTO>;

            Assert.AreEqual(404, objectResult?.StatusCode);
            //Assert.AreEqual(5, prg?.Count());
        }
        [TestMethod()]
        public async Task GetAllUserProgress_EmptyUnitAll()
        {
            IEnumerable<UserProgress> progress = _fixture.CreateMany<UserProgress>(5).ToList();
            Unit unit = null;
            var account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.GetUserProgress()).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByID(It.IsAny<int>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountById(It.IsAny<int>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.GetAllUserProgress();
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as IEnumerable<UserProgressDTO>;

            Assert.AreEqual(200, objectResult?.StatusCode);
            Assert.AreEqual(0, def?.Count());
        }
        [TestMethod()]
        public async Task GetAllUserProgress_EmptyAccAll()
        {
            IEnumerable<UserProgress> progress = _fixture.CreateMany<UserProgress>(5).ToList();
            Unit unit = _fixture.Create<Unit>();
            Account account = null;

            _progresRepoMock.Setup(repo => repo.GetUserProgress()).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByID(It.IsAny<int>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountById(It.IsAny<int>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.GetAllUserProgress();
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as IEnumerable<UserProgressDTO>;

            Assert.AreEqual(200, objectResult?.StatusCode);
            Assert.AreEqual(0, def?.Count());
        }
        [TestMethod()]
        public async Task GetAllUserProgress_EmptyUnitOne()
        {
            List<UserProgress> progress = _fixture.CreateMany<UserProgress>(3).ToList();
            Unit unitEmpty = null;
            Unit unit = _fixture.Create<Unit>();
            var account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.GetUserProgress()).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByID(progress[0].UnitId)).ReturnsAsync(unit);
            _unitRepoMock.Setup(repo => repo.GetUnitByID(progress[1].UnitId)).ReturnsAsync(unit);
            _unitRepoMock.Setup(repo => repo.GetUnitByID(progress[2].UnitId)).ReturnsAsync(unitEmpty);

            _accountRepoMock.Setup(repo => repo.GetAccountById(It.IsAny<int>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.GetAllUserProgress();
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as IEnumerable<UserProgressDTO>;

            Assert.AreEqual(200, objectResult?.StatusCode);
            Assert.AreEqual(2, def?.Count());
        }
        //Dla EmptyAccountOne nie testowane, ponieważ działa analogicznie, a test dla EmptyAccountAll działa poprawnie

        [TestMethod()]
        public async Task GetUserProgressByUser_Correct()
        { 
            var progress = _fixture.CreateMany<UserProgress>(5).ToList();
            var unit = _fixture.Create<Unit>();
            var account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.GetUserProgressByUserId(It.IsAny<int>())).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByID(It.IsAny<int>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountByName(It.IsAny<string>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.GetUserProgressByUser("Test");
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as IEnumerable<UserProgressDTO>;

            Assert.AreEqual(200, objectResult?.StatusCode);
            Assert.AreEqual(5, def?.Count());
        }

        [TestMethod()]
        public async Task GetUserProgressByUser_Exception()
        {
            var progress = _fixture.CreateMany<UserProgress>(5).ToList();
            var unit = _fixture.Create<Unit>();
            var account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.GetUserProgressByUserId(It.IsAny<int>())).ThrowsAsync(new Exception("Test exception"));
            _unitRepoMock.Setup(repo => repo.GetUnitByID(It.IsAny<int>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountByName(It.IsAny<string>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.GetUserProgressByUser("Test");
            var objectResult = result.Result as ObjectResult;

            Assert.AreEqual(400, objectResult?.StatusCode);
        }

        [TestMethod()]
        public async Task GetUserProgressByUser_EmptyProgress()
        {
            IEnumerable<UserProgress> progress = null;
            var unit = _fixture.Create<Unit>();
            var account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.GetUserProgressByUserId(It.IsAny<int>())).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByID(It.IsAny<int>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountByName(It.IsAny<string>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.GetUserProgressByUser("Test");
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }
        
        [TestMethod()]

        public async Task GetUserProgressByUser_EmptyUser()
        {
            IEnumerable<UserProgress> progress = _fixture.CreateMany<UserProgress>(5).ToList();
            var unit = _fixture.Create<Unit>();
            Account account = null;

            _progresRepoMock.Setup(repo => repo.GetUserProgressByUserId(It.IsAny<int>())).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByID(It.IsAny<int>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountByName(It.IsAny<string>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.GetUserProgressByUser("Test");
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }

         [TestMethod()]

        public async Task GetUserProgressByUser_EmptyUnitAll()
        {
            IEnumerable<UserProgress> progress = _fixture.CreateMany<UserProgress>(5).ToList();
            Unit unit = null;
            Account account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.GetUserProgressByUserId(It.IsAny<int>())).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByID(It.IsAny<int>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountByName(It.IsAny<string>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.GetUserProgressByUser("Test");
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as IEnumerable<UserProgressDTO>;

            Assert.AreEqual(200, objectResult?.StatusCode);
            Assert.AreEqual(0, def?.Count());
        }
        [TestMethod()]

        public async Task GetUserProgressByUser_EmptyUnitOne()
        {
            List<UserProgress> progress = _fixture.CreateMany<UserProgress>(3).ToList();
            Unit unitEmpty = null;
            Unit unit = _fixture.Create<Unit>();
            Account account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.GetUserProgressByUserId(It.IsAny<int>())).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByID(progress[0].UnitId)).ReturnsAsync(unit);
            _unitRepoMock.Setup(repo => repo.GetUnitByID(progress[1].UnitId)).ReturnsAsync(unitEmpty);
            _unitRepoMock.Setup(repo => repo.GetUnitByID(progress[2].UnitId)).ReturnsAsync(unit);

            _accountRepoMock.Setup(repo => repo.GetAccountByName(It.IsAny<string>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.GetUserProgressByUser("Test");
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as IEnumerable<UserProgressDTO>;

            Assert.AreEqual(200, objectResult?.StatusCode);
            Assert.AreEqual(2, def?.Count());
        }

          [TestMethod()]
        public async Task GetUserProgressByAll_Correct()
        {
            var progress = _fixture.Create<UserProgress>();
            var unit = _fixture.Create<Unit>();
            var account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.GetUserProgressByUserIdUnitIdType(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountByName(It.IsAny<string>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.GetUserProgressByAll("TestUser", "TestUnit", "TestType");
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as UserProgressDTO;

            Assert.AreEqual(200, objectResult?.StatusCode);
            Assert.AreEqual(unit.name, def?.unitName);
        }

        [TestMethod()]
        public async Task GetUserProgressByAll_Exception() 
        {
            var progress = _fixture.Create<UserProgress>();
            var unit = _fixture.Create<Unit>();
            var account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.GetUserProgressByUserIdUnitIdType(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).ThrowsAsync(new Exception("Test exception"));
            _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountByName(It.IsAny<string>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.GetUserProgressByAll("TestUser", "TestUnit","TestType");
            var objectResult = result.Result as ObjectResult;

            var def = objectResult?.Value as UserProgressDTO;

            Assert.AreEqual(400, objectResult?.StatusCode);
        }

      

        [TestMethod()]
        public async Task GetUserProgressByAll_EmptyProgress()
        {
            UserProgress progress = null;//_fixture.Create<UserProgress>();
            var unit = _fixture.Create<Unit>();
            var account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.GetUserProgressByUserIdUnitIdType(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountByName(It.IsAny<string>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.GetUserProgressByAll("TestUser", "TestUnit", "TestType");
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }
        [TestMethod()]
        public async Task GetUserProgressByAll_EmptyUnit()
        {
            UserProgress progress =_fixture.Create<UserProgress>();
            Unit unit = null;//_fixture.Create<Unit>();
            var account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.GetUserProgressByUserIdUnitIdType(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountByName(It.IsAny<string>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.GetUserProgressByAll("TestUser", "TestUnit", "TestType");
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }

        [TestMethod()]
        public async Task GetUserProgressByAll_EmptyUser()
        {
            UserProgress progress = _fixture.Create<UserProgress>();
            var unit = _fixture.Create<Unit>();
            Account account = null;//_fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.GetUserProgressByUserIdUnitIdType(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountByName(It.IsAny<string>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.GetUserProgressByAll("TestUser", "TestUnit", "TestType");
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }

        [TestMethod()]
        public async Task AddProgress_Correct()
        {
            var progress = _fixture.Create<UserProgress>();
            var progressDTO = _fixture.Create<UserProgressDTO>();
            var unit = _fixture.Create<Unit>();
            var account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.AddProgress(It.IsAny<UserProgress>())).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountById(It.IsAny<int>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.AddProgress(progressDTO);
            var objectResult = result.Result as ObjectResult;

            var prg = objectResult?.Value as UserProgressDTO;

            Assert.AreEqual(201, objectResult?.StatusCode);
            Assert.AreEqual(unit.name, prg?.unitName);
        }


        [TestMethod()]
        public async Task AddProgress_Exception()
        {
            //var progress = _fixture.Create<UserProgress>();
            var progressDTO = _fixture.Create<UserProgressDTO>();
            var unit = _fixture.Create<Unit>();
            var account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.AddProgress(It.IsAny<UserProgress>())).ThrowsAsync(new Exception("Test exception"));
            _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountById(It.IsAny<int>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.AddProgress(progressDTO);
            var objectResult = result.Result as ObjectResult;

            var prg = objectResult?.Value as UserProgressDTO;

            Assert.AreEqual(400, objectResult?.StatusCode);
            //Assert.AreEqual(unit.name, prg?.unitName);
        }

        [TestMethod()]
        public async Task AddProgress_EmptyProgress()
        {
            UserProgress progress = null; //_fixture.Create<UserProgress>();
            var progressDTO = _fixture.Create<UserProgressDTO>();
            var unit = _fixture.Create<Unit>();
            var account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.AddProgress(It.IsAny<UserProgress>())).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountById(It.IsAny<int>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.AddProgress(progressDTO);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }

        [TestMethod()]
        public async Task AddProgress_EmptyUnit()
        {
            UserProgress progress = _fixture.Create<UserProgress>();
            var progressDTO = _fixture.Create<UserProgressDTO>();
            Unit unit = null; //_fixture.Create<Unit>();
            var account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.AddProgress(It.IsAny<UserProgress>())).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountById(It.IsAny<int>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.AddProgress(progressDTO);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }
        [TestMethod()]
        public async Task AddProgress_EmptyAccount()
        {
            UserProgress progress = _fixture.Create<UserProgress>();
            var progressDTO = _fixture.Create<UserProgressDTO>();
            Unit unit = _fixture.Create<Unit>();
            Account account = null; // _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.AddProgress(It.IsAny<UserProgress>())).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountById(It.IsAny<int>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.AddProgress(progressDTO);
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }

        [TestMethod()]
        public async Task AddProgressNames_Correct()
        {
            var progress = _fixture.Create<UserProgress>();
            var progressDTO = _fixture.Create<UserProgressDTO>();
            var unit = _fixture.Create<Unit>();
            var account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.AddProgress(It.IsAny<UserProgress>())).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountByName(It.IsAny<string>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.AddProgressNames("test","test",0,0,"test");
            var objectResult = result.Result as ObjectResult;

            var prg = objectResult?.Value as UserProgressDTO;

            Assert.AreEqual(201, objectResult?.StatusCode);
            Assert.AreEqual(unit.name, prg?.unitName);
        }

        [TestMethod()]
        public async Task AddProgressNames_Exception()
        {
            //var progress = _fixture.Create<UserProgress>();
            var progressDTO = _fixture.Create<UserProgressDTO>();
            var unit = _fixture.Create<Unit>();
            var account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.AddProgress(It.IsAny<UserProgress>())).ThrowsAsync(new Exception("Test exception"));
            _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountByName(It.IsAny<string>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.AddProgressNames("test", "test", 0, 0, "test");
            var objectResult = result.Result as ObjectResult;

            var prg = objectResult?.Value as UserProgressDTO;

            Assert.AreEqual(400, objectResult?.StatusCode);
            //Assert.AreEqual(unit.name, prg?.unitName);
        }

        [TestMethod()]
        public async Task AddProgressNames_EmptyProgress()
        {
            UserProgress progress = null; //_fixture.Create<UserProgress>();
            var progressDTO = _fixture.Create<UserProgressDTO>();
            var unit = _fixture.Create<Unit>();
            var account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.AddProgress(It.IsAny<UserProgress>())).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountByName(It.IsAny<string>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.AddProgressNames("test", "test", 0, 0, "test");
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }

        [TestMethod()]
        public async Task AddProgressNames_EmptyUnit()
        {
            UserProgress progress = _fixture.Create<UserProgress>();
            var progressDTO = _fixture.Create<UserProgressDTO>();
            Unit unit = null; //_fixture.Create<Unit>();
            var account = _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.AddProgress(It.IsAny<UserProgress>())).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountByName(It.IsAny<string>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.AddProgressNames("test", "test", 0, 0, "test");
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }
        [TestMethod()]
        public async Task AddProgressNames_EmptyAccount()
        {
            UserProgress progress = _fixture.Create<UserProgress>();
            var progressDTO = _fixture.Create<UserProgressDTO>();
            Unit unit = _fixture.Create<Unit>();
            Account account = null; // _fixture.Create<Account>();

            _progresRepoMock.Setup(repo => repo.AddProgress(It.IsAny<UserProgress>())).ReturnsAsync(progress);
            _unitRepoMock.Setup(repo => repo.GetUnitByName(It.IsAny<string>())).ReturnsAsync(unit);
            _accountRepoMock.Setup(repo => repo.GetAccountByName(It.IsAny<string>())).ReturnsAsync(account);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.AddProgressNames("test", "test", 0, 0, "test");
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }

        [TestMethod()]
        public async Task UpdateProgress_Correct()
        {
            var progress = _fixture.Create<UserProgress>();
            var progressDTO = _fixture.Create<UserProgressDTO>();
           
            _progresRepoMock.Setup(repo => repo.UpdateProgress(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(progress);
           
            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.UpdateProgress(progressDTO);
            var objectResult = result.Result as ObjectResult;

            var prg = objectResult?.Value as UserProgressDTO;

            Assert.AreEqual(201, objectResult?.StatusCode);
            Assert.AreEqual(progress.Id, prg?.Id);
        }

        [TestMethod()]
        public async Task UpdateProgress_Exception()
        {
            var progress = _fixture.Create<UserProgress>();
            var progressDTO = _fixture.Create<UserProgressDTO>();

            _progresRepoMock.Setup(repo => repo.UpdateProgress(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception("Test exception"));
            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.UpdateProgress(progressDTO);
            var objectResult = result.Result as ObjectResult;


            Assert.AreEqual(400, objectResult?.StatusCode);
        }

        [TestMethod()]
        public async Task UpdateProgress_Empty()
        {
            UserProgress progress = null;
            var progressDTO = _fixture.Create<UserProgressDTO>();

            _progresRepoMock.Setup(repo => repo.UpdateProgress(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(progress);

            _controller = new UserProgressController(_progresRepoMock.Object, _accountRepoMock.Object, _unitRepoMock.Object);

            var result = await _controller.UpdateProgress(progressDTO);
            var objectResult = result.Result as StatusCodeResult;


            Assert.AreEqual(404, objectResult?.StatusCode);
        }
    }
}