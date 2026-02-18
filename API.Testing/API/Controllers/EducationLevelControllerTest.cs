using AutoFixture;
using DTO.DTOs;
using MathApp.Backend.API.Controllers;
using MathApp.Backend.API.Interfaces;
using MathApp.Backend.Data.Enteties;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MathApp.Testing.API.Controllers.Tests
{
    [TestClass]
    public class EducationLevelControllerTest
    {
        private Mock<IEducationLevelRepo> _educationLevelRepo;
        private Fixture _fixture;
        private EducationLevelController _controller;
        public EducationLevelControllerTest()
        {
            _educationLevelRepo = new Mock<IEducationLevelRepo>();
            _fixture = new Fixture();
        }

        [TestMethod]
        public async Task GetEducationLevels_Correct()
        {
            var edLevels = _fixture.CreateMany<EducationLevel>(5).ToList();
            _educationLevelRepo.Setup(repo => repo.GetAllEducationLevels()).ReturnsAsync(edLevels);
            _controller = new EducationLevelController(_educationLevelRepo.Object);

            var result = await _controller.GetEducationLevels();
            var objectResult = result.Result as ObjectResult;

            var acc = objectResult?.Value as IEnumerable<EducationLevelDTO>;

            Assert.AreEqual(200, objectResult?.StatusCode);
            Assert.AreEqual(5, acc?.Count());
        }
        [TestMethod]
        public async Task GetAccounts_Exeption()
        {
            _educationLevelRepo.Setup(repo => repo.GetAllEducationLevels()).ThrowsAsync(new Exception("Test exception"));
            _controller = new EducationLevelController(_educationLevelRepo.Object);

            var result = await _controller.GetEducationLevels();
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(400, objectResult?.StatusCode);
        }

        [TestMethod]
        public async Task GetAccounts_Empty()
        {
            List<EducationLevel>? edLvl = null;
            _educationLevelRepo.Setup(repo => repo.GetAllEducationLevels()).ReturnsAsync(edLvl);
            _controller = new EducationLevelController(_educationLevelRepo.Object);

            var result = await _controller.GetEducationLevels();
            var objectResult = result.Result as StatusCodeResult;

            Assert.AreEqual(404, objectResult?.StatusCode);
        }
    }
}
