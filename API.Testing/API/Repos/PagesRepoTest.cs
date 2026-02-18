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
    public class PagesRepoTest
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
        public async Task GetAllPages_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new PagesRepo(context);
            var pages = _fixture.CreateMany<Pages>(5);
            await context.Pages.AddRangeAsync(pages);
            context.SaveChanges();

            var result = await repository.GetAllPages();

            Assert.IsNotNull(result);
            Assert.AreEqual(pages.ToList()[1].Id, result.ToList()[1].Id);
            Assert.AreEqual(5, result.Count());
        }

        [TestMethod()]
        public async Task GetAllPages_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new PagesRepo(context);
          

            var result = await repository.GetAllPages();

            Assert.IsNotNull(result);
          //  Assert.AreEqual(pages.ToList()[1].Id, result.ToList()[1].Id);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod()]
        public async Task GetPagesByUnit_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new PagesRepo(context);
            var pages = _fixture.CreateMany<Pages>(5).ToList();
            pages[0].UnitID = 1;
            pages[1].UnitID = 1;
            pages[2].UnitID = 1;
            pages[3].UnitID = 2;
            pages[4].UnitID = 2;
            await context.Pages.AddRangeAsync(pages);
            context.SaveChanges();

            var result = await repository.GetPagesByUnit(1);

            Assert.IsNotNull(result);
           
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(5, context.Pages.Count());
        }
        [TestMethod()]
        public async Task GetPagesByUnit_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new PagesRepo(context);
            var pages = _fixture.CreateMany<Pages>(5).ToList();
            pages[0].UnitID = 1;
            pages[1].UnitID = 1;
            pages[2].UnitID = 1;
            pages[3].UnitID = 2;
            pages[4].UnitID = 2;
            await context.Pages.AddRangeAsync(pages);
            context.SaveChanges();

            var result = await repository.GetPagesByUnit(4);

            Assert.IsNotNull(result);

            Assert.AreEqual(0, result.Count());
            Assert.AreEqual(5, context.Pages.Count());
        }
        [TestMethod()]
        public async Task AddPage_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new PagesRepo(context);
            var page = _fixture.Create<Pages>();

            var result = await repository.AddPage(page);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, context.Pages.Count());
        }

        [TestMethod()]
        public async Task UpdatePage_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new PagesRepo(context);
            var page = _fixture.Create<Pages>();
            page.Id = 1;
            await context.Pages.AddAsync(page);
            context.SaveChanges();

            var result = await repository.UpdatePage(1,"name","link",2);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, context.Pages.Count());
        }

        [TestMethod()]
        public async Task UpdatePage_DoesntExist()
        {
            using var context = new DataBase(_options);
            var repository = new PagesRepo(context);
            var page = _fixture.Create<Pages>();
            page.Id = 1;
           await context.Pages.AddAsync(page);
            context.SaveChanges();

            var result = await repository.UpdatePage(4, "name", "link", 2);

            Assert.IsNull(result);
            Assert.AreEqual(1, context.Pages.Count());
        }

        [TestMethod()]
        public async Task DeletePage_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new PagesRepo(context);
            var page = _fixture.CreateMany<Pages>(2).ToList();
            page[0].Id = 1;
            page[1].Id = 2;

            await context.Pages.AddRangeAsync(page);
            context.SaveChanges();

            var result = await repository.DeletePage(1);

            Assert.IsTrue(result);
            Assert.AreEqual(1, context.Pages.Count());
        }
        [TestMethod()]
        public async Task DeletePage_DoesntExist()
        {
            using var context = new DataBase(_options);
            var repository = new PagesRepo(context);
            var page = _fixture.CreateMany<Pages>(2).ToList();
            page[0].Id = 1;
            page[1].Id = 2;

            await context.Pages.AddRangeAsync(page);
            context.SaveChanges();

            var result = await repository.DeletePage(4);

            Assert.IsFalse(result);
            Assert.AreEqual(2, context.Pages.Count());
        }
    }
}