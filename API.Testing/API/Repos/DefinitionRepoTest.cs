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
    public class DefinitionRepoTest
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
        public async Task AddDefinition_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new DefinitionRepo(context);
            var newDef = _fixture.Create<Definition>();

            var result = await repository.AddDefinition(newDef);

            Assert.IsNotNull(result);
            Assert.AreEqual(newDef.Name, result.Name);
            Assert.AreEqual(1, context.Definitions.Count());
        }

        [TestMethod()]
        public async Task AddDefinition_RepeatID()
        {
            using var context = new DataBase(_options);
            var repository = new DefinitionRepo(context);
            var newDef = _fixture.Create<Definition>();
            newDef.Id = 1;
            context.Definitions.Add(newDef);
            context.SaveChanges();

            var newDef2 = _fixture.Create<Definition>();
            newDef2.Id = 1;

            var result = await repository.AddDefinition(newDef);

            Assert.IsNull(result);
            //Assert.AreEqual(newDef.Name, result.Name);
            Assert.AreEqual(1, context.Definitions.Count());
        }
            [TestMethod()]
        public async Task EditDefinition_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new DefinitionRepo(context);
            var newDef = _fixture.Create<Definition>();
            newDef.Name = "OriginalName";
            context.Definitions.Add(newDef);
            context.SaveChanges();

            var result = await repository.EditDefinition(newDef.Id,"EditedName","Type","p1","p2",1);

            Assert.IsTrue(result);
            var updated = await context.Definitions.FirstAsync(a => a.Id == newDef.Id);
            Assert.AreEqual("EditedName", updated.Name);
            Assert.AreEqual(1, context.Definitions.Count());
        }

        [TestMethod()]
        public async Task EditDefinition_DoesntExist()
        {
            using var context = new DataBase(_options);
            var repository = new DefinitionRepo(context);
            var newDef = _fixture.Create<Definition>();
            newDef.Id = 1;
            newDef.Name = "OriginalName";
            context.Definitions.Add(newDef);
            context.SaveChanges();

            var result = await repository.EditDefinition(2, "EditedName", "Type", "p1", "p2", 1);

            Assert.IsFalse(result);
            var updated = await context.Definitions.FirstAsync(a => a.Id == newDef.Id);
            Assert.AreNotEqual("EditedName", updated.Name);
            Assert.AreEqual(1, context.Definitions.Count());
        }

        [TestMethod()]
        public async Task GetAllDefinitions_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new DefinitionRepo(context);
            var newDef = _fixture.Create<Definition>();
            context.Definitions.Add(newDef);
            context.SaveChanges();

            var result = await repository.GetAllDefinitions();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, context.Definitions.Count());
        }
        [TestMethod()]
        public async Task GetAllDefinitions_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new DefinitionRepo(context);

            var result = await repository.GetAllDefinitions();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, context.Definitions.Count());
        }

        // GetDefinitionbyId and GetDefinitionbyName are not tested as they are not used in the API and are not needed for the functionality of the app. They can be added if needed in the future.

        [TestMethod()]
        public async Task GetDefinitionsByUnit_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new DefinitionRepo(context);
            var newDef = _fixture.Create<Definition>();
            Definition newDef2 = _fixture.Create<Definition>();
            Definition newDef3 = _fixture.Create<Definition>();

            newDef.unitId = 1;
            newDef2.unitId = 1;
            newDef3.unitId = 2;

            context.Definitions.Add(newDef);
            context.Definitions.Add(newDef2);
            context.Definitions.Add(newDef3);

            context.SaveChanges();

            var result = await repository.GetDefinitionsByUnit(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(3, context.Definitions.Count());
        }

        [TestMethod()]
        public async Task GetDefinitionsByUnit_Empty()
        {
            using var context = new DataBase(_options);
            var repository = new DefinitionRepo(context);
            var newDef = _fixture.Create<Definition>();
            Definition newDef2 = _fixture.Create<Definition>();
            Definition newDef3 = _fixture.Create<Definition>();

            newDef.unitId = 2;
            newDef2.unitId = 2;
            newDef3.unitId = 2;

            context.Definitions.Add(newDef);
            context.Definitions.Add(newDef2);
            context.Definitions.Add(newDef3);

            context.SaveChanges();

            var result = await repository.GetDefinitionsByUnit(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
            Assert.AreEqual(3, context.Definitions.Count());
        }

        [TestMethod()]
        public async Task RemoveDefinition_Correct()
        {
            using var context = new DataBase(_options);
            var repository = new DefinitionRepo(context);
            var newDef = _fixture.Create<Definition>();
            Definition newDef2 = _fixture.Create<Definition>();
            Definition newDef3 = _fixture.Create<Definition>();

            newDef.Id = 1;
            newDef2.Id = 2;
            newDef3.Id = 3;

            context.Definitions.Add(newDef);
            context.Definitions.Add(newDef2);
            context.Definitions.Add(newDef3);

            context.SaveChanges();

            var result = await repository.RemoveDefinition(1);

            Assert.IsTrue(result);
            //Assert.AreEqual(2, result.Count());
            Assert.AreEqual(2, context.Definitions.Count());
        }
        [TestMethod()]
        public async Task RemoveDefinition_DoesntExist()
        {
            using var context = new DataBase(_options);
            var repository = new DefinitionRepo(context);
            var newDef = _fixture.Create<Definition>();
            Definition newDef2 = _fixture.Create<Definition>();
            Definition newDef3 = _fixture.Create<Definition>();

            newDef.Id = 1;
            newDef2.Id = 2;
            newDef3.Id = 3;

            context.Definitions.Add(newDef);
            context.Definitions.Add(newDef2);
            context.Definitions.Add(newDef3);

            context.SaveChanges();

            var result = await repository.RemoveDefinition(4);

            Assert.IsFalse(result);
            //Assert.AreEqual(2, result.Count());
            Assert.AreEqual(3, context.Definitions.Count());
        }
    }
}