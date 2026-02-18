using AutoFixture;
using MathApp.Backend.API.Repos;
using MathApp.Backend.Data.Enteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathApp.Testing.API.Repos.Tests
{
    [TestClass()]
    public class EducationLevelRepoTest
    {
        private DbContextOptions<DataBase> _options;
        private Fixture _fixture;

        [TestInitialize]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<DataBase>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _fixture = new Fixture();

        }

        [TestMethod()]
        public async Task GetAllEducationLevels_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new EducationLevelRepo(context);
            var edLevels = _fixture.CreateMany<EducationLevel>(5);
            await context.educationLevels.AddRangeAsync(edLevels);
            context.SaveChanges();

            var result = await repository.GetAllEducationLevels();

            Assert.IsNotNull(result);
            Assert.AreEqual(edLevels.ToList()[1].Id, result.ToList()[1].Id);
            Assert.AreEqual(5, context.educationLevels.Count());
        }
        [TestMethod()]
        public async Task GetAllEducationLevels_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new EducationLevelRepo(context);
           

            var result = await repository.GetAllEducationLevels();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, context.educationLevels.Count());
        }

        [TestMethod()]
        public async Task GetEducationLevelByID_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new EducationLevelRepo(context);
            var edLevels = _fixture.CreateMany<EducationLevel>(5).ToList();
            edLevels[0].Id = 1;
            edLevels[1].Id = 2;
            edLevels[2].Id = 3;
            edLevels[3].Id = 4;
            edLevels[4].Id = 5;

            await context.educationLevels.AddRangeAsync(edLevels);
            context.SaveChanges();

            var result = await repository.GetEducationLevelByID(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(edLevels[0].name,result.name);
        }
        [TestMethod()]
        public async Task GetEducationLevelByID_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new EducationLevelRepo(context);
            var edLevels = _fixture.CreateMany<EducationLevel>(5).ToList();
            edLevels[0].Id = 1;
            edLevels[1].Id = 2;
            edLevels[2].Id = 3;
            edLevels[3].Id = 4;
            edLevels[4].Id = 5;

            await context.educationLevels.AddRangeAsync(edLevels);
            context.SaveChanges();

            var result = await repository.GetEducationLevelByID(6);

            Assert.IsNull(result);
        }


        [TestMethod()]
        public async Task GetEducationLevelsbyName_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new EducationLevelRepo(context);
            var edLevels = _fixture.CreateMany<EducationLevel>(5).ToList();
            edLevels[0].name = "1";
            edLevels[1].name = "2";
            edLevels[2].name = "3";
            edLevels[3].name = "4";
            edLevels[4].name = "5";

            await context.educationLevels.AddRangeAsync(edLevels);
            context.SaveChanges();

            var result = await repository.GetEducationLevelsbyName(edLevels[0].name);

            Assert.IsNotNull(result);
            Assert.AreEqual(edLevels[0].Id, result.Id);
        }

        [TestMethod()]
        public async Task GetEducationLevelsbyName_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new EducationLevelRepo(context);
            var edLevels = _fixture.CreateMany<EducationLevel>(5).ToList();
            edLevels[0].name = "1";
            edLevels[1].name = "2";
            edLevels[2].name = "3";
            edLevels[3].name = "4";
            edLevels[4].name = "5";

            await context.educationLevels.AddRangeAsync(edLevels);
            context.SaveChanges();

            var result = await repository.GetEducationLevelsbyName("12");

            Assert.IsNull(result);
        }
        [TestMethod()]
        public async Task GetEducationLevelName_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new EducationLevelRepo(context);
            var edLevels = _fixture.CreateMany<EducationLevel>(5).ToList();
            edLevels[0].Id = 1;
            edLevels[1].Id = 2;
            edLevels[2].Id = 3;
            edLevels[3].Id = 4;
            edLevels[4].Id = 5;

            await context.educationLevels.AddRangeAsync(edLevels);
            context.SaveChanges();

            var result = await repository.GetEducationLevelName(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(edLevels[0].name, result);
        }

        [TestMethod()]
        public async Task GetEducationLevelName_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new EducationLevelRepo(context);
            var edLevels = _fixture.CreateMany<EducationLevel>(5).ToList();
            edLevels[0].Id = 1;
            edLevels[1].Id = 2;
            edLevels[2].Id = 3;
            edLevels[3].Id = 4;
            edLevels[4].Id = 5;

            await context.educationLevels.AddRangeAsync(edLevels);
            context.SaveChanges();

            var result = await repository.GetEducationLevelName(0);

            Assert.IsNull(result);
        }
    }
}