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
    public class UserProgressRepoTest
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
        public async Task GetUserProgress_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new UserProgressRepo(context);
            var progr = _fixture.CreateMany<UserProgress>(5);
            await context.UserProgresses.AddRangeAsync(progr);
            context.SaveChanges();

            var result = await repository.GetUserProgress();

            Assert.IsNotNull(result);
            Assert.AreEqual(progr.ToList()[1].Id, result.ToList()[1].Id);
            Assert.AreEqual(5, result.Count());
        }

        [TestMethod()]
        public async Task GetUserProgress_Incorrect()
        {
            using var context = new DataBase(_options);
            var repository = new UserProgressRepo(context);

            var result = await repository.GetUserProgress();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod()]
        public async Task GetUserProgressByUserId_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new UserProgressRepo(context);
            var progr = _fixture.CreateMany<UserProgress>(3).ToList();
            progr[0].AccountId = 1;
            progr[1].AccountId = 1;
            progr[2].AccountId = 2;

            await context.UserProgresses.AddRangeAsync(progr);
            context.SaveChanges();

            var result = await repository.GetUserProgressByUserId(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(3, context.UserProgresses.Count());

        }

        [TestMethod()]
        public async Task GetUserProgressByUserId_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new UserProgressRepo(context);
            var progr = _fixture.CreateMany<UserProgress>(3).ToList();
            progr[0].AccountId = 1;
            progr[1].AccountId = 1;
            progr[2].AccountId = 2;

            await context.UserProgresses.AddRangeAsync(progr);
            context.SaveChanges();

            var result = await repository.GetUserProgressByUserId(4);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
            Assert.AreEqual(3, context.UserProgresses.Count());

        }

        [TestMethod()]
        public async Task GetUserProgressByUserIdUnitIdType_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new UserProgressRepo(context);
            var progr = _fixture.CreateMany<UserProgress>(3).ToList();
            progr[0].AccountId = 1;
            progr[0].UnitId = 2;
            progr[0].type = "t1";

            progr[1].AccountId = 1;
            progr[1].UnitId = 1;
            progr[1].type = "t1";

            progr[2].AccountId = 2;
            progr[2].UnitId = 2;
            progr[2].type = "t2";

            await context.UserProgresses.AddRangeAsync(progr);
            context.SaveChanges();

            var result = await repository.GetUserProgressByUserIdUnitIdType(1,1,"t1");

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task GetUserProgressByUserIdUnitIdType_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new UserProgressRepo(context);
            var progr = _fixture.CreateMany<UserProgress>(3).ToList();
            progr[0].AccountId = 1;
            progr[0].UnitId = 2;
            progr[0].type = "t1";

            progr[1].AccountId = 1;
            progr[1].UnitId = 1;
            progr[1].type = "t1";

            progr[2].AccountId = 2;
            progr[2].UnitId = 2;
            progr[2].type = "t2";

            await context.UserProgresses.AddRangeAsync(progr);
            context.SaveChanges();

            var result = await repository.GetUserProgressByUserIdUnitIdType(14, 1, "t1");

            Assert.IsNull(result);
        }

        [TestMethod()]
        public async Task AddProgress_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new UserProgressRepo(context);
            var progr = _fixture.Create<UserProgress>();

            var result = await repository.AddProgress(progr);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, context.UserProgresses.Count());
        }

        [TestMethod()]
        public async Task UpdateProgress_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new UserProgressRepo(context);
            var progr = _fixture.Create<UserProgress>();
            progr.good = 1;
            await context.UserProgresses.AddAsync(progr);
            context.SaveChanges();

            var result = await repository.UpdateProgress(progr.Id,"Hello",12,12);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, context.UserProgresses.Count());
        }

        [TestMethod()]
        public async Task UpdateProgress_DoesntExist()
        {
            using var context = new DataBase(_options);
            var repository = new UserProgressRepo(context);
            var progr = _fixture.Create<UserProgress>();
            progr.Id = 1;
            await context.UserProgresses.AddAsync(progr);
            context.SaveChanges();

            var result = await repository.UpdateProgress(12, "Hello", 12, 12);

            Assert.IsNull(result);
            Assert.AreEqual(1, context.UserProgresses.Count());
        }
    }
}