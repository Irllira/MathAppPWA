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
    public class IncorrectDefinitionRepoTest
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
        public async Task GetAllIncorrect_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new IncorrectDefinitionRepo(context);
            var incorrects = _fixture.CreateMany<IncorrectDefinition>(5);
            await context.IncorrectDefinitions.AddRangeAsync(incorrects);
            context.SaveChanges();

            var result = await repository.GetAllIncorrect();

            Assert.IsNotNull(result);
            Assert.AreEqual(incorrects.ToList()[1].Id, result.ToList()[1].Id);
            Assert.AreEqual(5, result.Count());
        }
        [TestMethod()]
        public async Task GetAllIncorrect_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new IncorrectDefinitionRepo(context);

            var result = await repository.GetAllIncorrect();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod()]
        public async Task GetIncorrectByID_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new IncorrectDefinitionRepo(context);
            var incorrects = _fixture.CreateMany<IncorrectDefinition>(5).ToList();
            incorrects[0].Id = 1;
            incorrects[1].Id = 5;
            incorrects[2].Id = 4;
            incorrects[3].Id = 3;
            incorrects[4].Id = 2;

            await context.IncorrectDefinitions.AddRangeAsync(incorrects);
            context.SaveChanges();

            var result = await repository.GetIncorrectByID(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(incorrects[0].Content, result.Content);
        }
        [TestMethod()]
        public async Task GetIncorrectByID_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new IncorrectDefinitionRepo(context);
            var incorrects = _fixture.CreateMany<IncorrectDefinition>(5).ToList();
            incorrects[0].Id = 1;
            incorrects[1].Id = 5;
            incorrects[2].Id = 4;
            incorrects[3].Id = 3;
            incorrects[4].Id = 2;

            await context.IncorrectDefinitions.AddRangeAsync(incorrects);
            context.SaveChanges();

            var result = await repository.GetIncorrectByID(6);

            Assert.IsNull(result);
            //Assert.AreEqual(incorrects[0].Content, result.Content);
            //Assert.AreEqual(5, result.Count());
        }
        [TestMethod()]
        public async Task GetIncorrectByContent_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new IncorrectDefinitionRepo(context);
            var incorrects = _fixture.CreateMany<IncorrectDefinition>(5).ToList();
            incorrects[0].Content = "1";
            incorrects[1].Content = "2";
            incorrects[2].Content = "3";
            incorrects[3].Content = "4";
            incorrects[4].Content = "5";

            await context.IncorrectDefinitions.AddRangeAsync(incorrects);
            context.SaveChanges();

            var result = await repository.GetIncorrectByContent("1");

            Assert.IsNotNull(result);
            Assert.AreEqual(incorrects[0].Id, result.Id);
        }

        [TestMethod()]
        public async Task GetIncorrectByContent_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new IncorrectDefinitionRepo(context);
            var incorrects = _fixture.CreateMany<IncorrectDefinition>(5).ToList();
            incorrects[0].Content = "1";
            incorrects[1].Content = "2";
            incorrects[2].Content = "3";
            incorrects[3].Content = "4";
            incorrects[4].Content = "5";

            await context.IncorrectDefinitions.AddRangeAsync(incorrects);
            context.SaveChanges();

            var result = await repository.GetIncorrectByContent("test");

            Assert.IsNull(result);
            //Assert.AreEqual(incorrects[0].Id, result.Id);
        }

        [TestMethod()]
        public async Task GetIncorrectsByDefinition_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new IncorrectDefinitionRepo(context);
            var pairs = _fixture.CreateMany<DefIncPair>(5).ToList();
            pairs[0].DefinitionId = 1;
            pairs[1].DefinitionId = 1;
            pairs[2].DefinitionId = 1;
            pairs[3].DefinitionId = 4;
            pairs[4].DefinitionId = 5;
            await context.DefIncPair.AddRangeAsync(pairs);
            context.SaveChanges();

            var result = await repository.GetIncorrectsByDefinition(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(5, context.DefIncPair.Count());
            Assert.AreEqual(3, result.Count());
        }
        [TestMethod()]
        public async Task GetIncorrectsByDefinition_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new IncorrectDefinitionRepo(context);
            var pairs = _fixture.CreateMany<DefIncPair>(5).ToList();
            pairs[0].DefinitionId = 1;
            pairs[1].DefinitionId = 1;
            pairs[2].DefinitionId = 1;
            pairs[3].DefinitionId = 4;
            pairs[4].DefinitionId = 5;
            await context.DefIncPair.AddRangeAsync(pairs);
            context.SaveChanges();

            var result = await repository.GetIncorrectsByDefinition(6);

            Assert.IsNotNull(result);
            Assert.AreEqual(5, context.DefIncPair.Count());
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod()]
        public async Task GetAllPairs_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new IncorrectDefinitionRepo(context);
            var pairs = _fixture.CreateMany<DefIncPair>(5).ToList();
            await context.DefIncPair.AddRangeAsync(pairs);
            context.SaveChanges();

            var result = await repository.GetAllPairs();

            Assert.IsNotNull(result);
            Assert.AreEqual(pairs[0].Id, result.ToList()[0].Id);
        }
        [TestMethod()]
        public async Task GetAllPairs_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new IncorrectDefinitionRepo(context);

            var result = await repository.GetAllPairs();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod()]
        public async Task GetPairsByDefinition_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new IncorrectDefinitionRepo(context);
            var pairs = _fixture.CreateMany<DefIncPair>(5).ToList();
            pairs[0].DefinitionId = 1;
            pairs[1].DefinitionId = 1;
            pairs[2].DefinitionId = 1;
            pairs[3].DefinitionId = 4;
            pairs[4].DefinitionId = 5;
            await context.DefIncPair.AddRangeAsync(pairs);
            context.SaveChanges();

            var result = await repository.GetPairsByDefinition(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(5, context.DefIncPair.Count());
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod()]
        public async Task GetPairsByDefinition_Incorrect()
        {
            using var context = new DataBase(_options);
            var repository = new IncorrectDefinitionRepo(context);
            var pairs = _fixture.CreateMany<DefIncPair>(5).ToList();
            pairs[0].DefinitionId = 1;
            pairs[1].DefinitionId = 1;
            pairs[2].DefinitionId = 1;
            pairs[3].DefinitionId = 4;
            pairs[4].DefinitionId = 5;
            await context.DefIncPair.AddRangeAsync(pairs);
            context.SaveChanges();

            var result = await repository.GetPairsByDefinition(2);

            Assert.IsNotNull(result);
            Assert.AreEqual(5, context.DefIncPair.Count());
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod()]
        public async Task AddIncorrect_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new IncorrectDefinitionRepo(context);
            var incorrect = _fixture.Create<IncorrectDefinition>();

     
            var result = await repository.AddIncorrect(incorrect);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, context.IncorrectDefinitions.Count());
        }
        [TestMethod()]
        public async Task AddIncorrect_Repeated()
        {
            using var context = new DataBase(_options);
            var repository = new IncorrectDefinitionRepo(context);
            var incorrect = _fixture.Create<IncorrectDefinition>();
            context.IncorrectDefinitions.Add(incorrect);
            context.SaveChanges();

            var result = await repository.AddIncorrect(incorrect);

            Assert.IsNull(result);
            Assert.AreEqual(1, context.IncorrectDefinitions.Count());
        }

        [TestMethod()]
        public async Task AddPair_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new IncorrectDefinitionRepo(context);
           /* var pairs = _fixture.CreateMany<DefIncPair>(5).ToList();
          
            await context.DefIncPair.AddRangeAsync(pairs);
            context.SaveChanges();
           */
            var result = await repository.AddPair(1,2);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, context.DefIncPair.Count());
        }

        [TestMethod()]
        public async Task DeletePair_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new IncorrectDefinitionRepo(context);
            var pairs = _fixture.CreateMany<DefIncPair>(5).ToList();
            pairs[0].DefinitionId = 1;
            pairs[0].IncorrectDefinitionId = 2;

            await context.DefIncPair.AddRangeAsync(pairs);
            context.SaveChanges();
            
            var result = await repository.DeletePair(1, 2);

            Assert.IsTrue(result);
            Assert.AreEqual(4, context.DefIncPair.Count());
        }

        [TestMethod()]
        public async Task DeletePair_DoesntExist()
        {
            using var context = new DataBase(_options);
            var repository = new IncorrectDefinitionRepo(context);
            var pairs = _fixture.CreateMany<DefIncPair>(2).ToList();
            pairs[0].DefinitionId = 2;
            pairs[0].IncorrectDefinitionId = 2;
            pairs[1].DefinitionId = 1;
            pairs[1].IncorrectDefinitionId = 1;
            await context.DefIncPair.AddRangeAsync(pairs);
            context.SaveChanges();

            var result = await repository.DeletePair(1, 2);

            Assert.IsFalse(result);
            Assert.AreEqual(2, context.DefIncPair.Count());
        }
    }
}