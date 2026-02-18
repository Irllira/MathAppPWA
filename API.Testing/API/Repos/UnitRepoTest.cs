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
    public class UnitRepoTest
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
        public async Task GetAllUnit_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new UnitRepo(context);
            var unit = _fixture.CreateMany<Unit>(5);
            await context.Units.AddRangeAsync(unit);
            context.SaveChanges();

            var result = await repository.GetAllUnit();

            Assert.IsNotNull(result);
            Assert.AreEqual(unit.ToList()[1].Id, result.ToList()[1].Id);
            Assert.AreEqual(5, result.Count());
        }

        [TestMethod()]
        public async Task GetAllUnit_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new UnitRepo(context);
            

            var result = await repository.GetAllUnit();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod()]
        public async Task GetUnitByID_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new UnitRepo(context);
            var unit = _fixture.CreateMany<Unit>(5).ToList();
            unit[0].Id = 1;
            unit[1].Id = 2;
            unit[2].Id = 3;
            unit[3].Id = 4;
            unit[4].Id = 5;

            await context.Units.AddRangeAsync(unit);
            context.SaveChanges();

            var result = await repository.GetUnitByID(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(unit.ToList()[0].name, result.name);
        }
        [TestMethod()]
        public async Task GetUnitByID_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new UnitRepo(context);
            var unit = _fixture.CreateMany<Unit>(5).ToList();
            unit[0].Id = 1;
            unit[1].Id = 2;
            unit[2].Id = 3;
            unit[3].Id = 4;
            unit[4].Id = 5;

            await context.Units.AddRangeAsync(unit);
            context.SaveChanges();

            var result = await repository.GetUnitByID(6);

            Assert.IsNull(result);
        }

        [TestMethod()]
        public async Task GetUnitByName_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new UnitRepo(context);
            var unit = _fixture.CreateMany<Unit>(5).ToList();
            unit[0].name = "Test1";
            unit[1].name = "Test2";
            unit[2].name = "Test3";
            unit[3].name = "Test4";
            unit[4].name = "Test5";

            await context.Units.AddRangeAsync(unit);
            context.SaveChanges();

            var result = await repository.GetUnitByName("Test1");

            Assert.IsNotNull(result);
            Assert.AreEqual(unit.ToList()[0].Id, result.Id);
        }

        [TestMethod()]
        public async Task GetUnitByName_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new UnitRepo(context);
            var unit = _fixture.CreateMany<Unit>(5).ToList();
            unit[0].name = "Test1";
            unit[1].name = "Test2";
            unit[2].name = "Test3";
            unit[3].name = "Test4";
            unit[4].name = "Test5";

            await context.Units.AddRangeAsync(unit);
            context.SaveChanges();

            var result = await repository.GetUnitByName("Test7");

            Assert.IsNull(result);
            //Assert.AreEqual(unit.ToList()[0].Id, result.Id);
        }

        [TestMethod()]
        public async Task AddUnit_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new UnitRepo(context);
            var unit = _fixture.Create<Unit>();

            var result = await repository.AddUnit(unit);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, context.Units.Count());
        }

        [TestMethod()]
        public async Task RemoveUnit_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new UnitRepo(context);
            var unit = _fixture.Create<Unit>();
            unit.educationLevelId = 1;
            await context.Units.AddRangeAsync(unit);
            context.SaveChanges();

            var result = await repository.RemoveUnit(unit.Id);

            Assert.IsTrue(result);
            Assert.AreEqual(0, context.Units.Count());
        }

        [TestMethod()]
        public async Task RemoveUnit_DoesntExist()
        {
            using var context = new DataBase(_options);
            var repository = new UnitRepo(context);
            var unit = _fixture.Create<Unit>();
            unit.Id = 1;
            await context.Units.AddRangeAsync(unit);
            context.SaveChanges();

            var result = await repository.RemoveUnit(2);

            Assert.IsFalse(result);
            Assert.AreEqual(1, context.Units.Count());
        }

        [TestMethod()]
        public async Task RemoveUnitByName_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new UnitRepo(context);
            var unit = _fixture.Create<Unit>();
            unit.name = "Name";
            await context.Units.AddAsync(unit);
            context.SaveChanges();

            var result = await repository.RemoveUnitByName("Name");

            Assert.IsTrue(result);
            Assert.AreEqual(0, context.Units.Count());
        }
        [TestMethod()]
        public async Task RemoveUnitByName_DoesntExist()
        {
            using var context = new DataBase(_options);
            var repository = new UnitRepo(context);
            var unit = _fixture.Create<Unit>();
            unit.name = "Name";
            await context.Units.AddAsync(unit);
            context.SaveChanges();

            var result = await repository.RemoveUnitByName("DifferentName");

            Assert.IsFalse(result);
            Assert.AreEqual(1, context.Units.Count());
        }
        [TestMethod()]
        public async Task GetUnitsbyEdLevel_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new UnitRepo(context);
            var unit = _fixture.CreateMany<Unit>(5).ToList();
            unit[0].educationLevelId = 1;
            unit[1].educationLevelId = 1;
            unit[2].educationLevelId = 1;
            unit[3].educationLevelId = 2;
            unit[4].educationLevelId = 2;

            await context.Units.AddRangeAsync(unit);
            context.SaveChanges();

            var result = await repository.GetUnitsbyEdLevel(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(5, context.Units.Count());
        }
        [TestMethod()]
        public async Task GetUnitsbyEdLevel_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new UnitRepo(context);
            var unit = _fixture.CreateMany<Unit>(5).ToList();
            unit[0].educationLevelId = 1;
            unit[1].educationLevelId = 1;
            unit[2].educationLevelId = 1;
            unit[3].educationLevelId = 2;
            unit[4].educationLevelId = 2;

            await context.Units.AddRangeAsync(unit);
            context.SaveChanges();

            var result = await repository.GetUnitsbyEdLevel(5);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
            Assert.AreEqual(5, context.Units.Count());
        }
        [TestMethod()]
        public async Task EditUnit_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new UnitRepo(context);
            var unit = _fixture.Create<Unit>();
            unit.educationLevelId = 1;
            await context.Units.AddRangeAsync(unit);
            context.SaveChanges();

            var result = await repository.EditUnit(unit.Id,"Hello","World",5);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, context.Units.Count());
        }

        [TestMethod()]
        public async Task EditUnit_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new UnitRepo(context);
            var unit = _fixture.Create<Unit>();
            unit.educationLevelId = 1;
            unit.Id = 1;
            await context.Units.AddRangeAsync(unit);
            context.SaveChanges();

            var result = await repository.EditUnit(0, "Hello", "World", 5);

            Assert.IsNull(result);
            Assert.AreEqual(1, context.Units.Count());
        }
    }
}