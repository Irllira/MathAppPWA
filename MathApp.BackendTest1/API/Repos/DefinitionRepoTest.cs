using MathApp.Backend.API.Repos;
using MathApp.Backend.Data.Enteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathApp.Backend.API.Repos.Tests
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
        public void AddDefinition_()
        {

        }

        [TestMethod()]
        public void AddDefinition_1()
        {

        }

        [TestMethod()]
        public void EditDefinition_()
        {

        }

        [TestMethod()]
        public void GetAllDefinitions_()
        {

        }

        [TestMethod()]
        public void GetDefinitionbyId_()
        {

        }

        [TestMethod()]
        public void GetDefinitionbyName_()
        {

        }

        [TestMethod()]
        public void GetDefinitionsByUnit_()
        {

        }

        [TestMethod()]
        public void GetDefinitionsByUnit_1()
        {

        }

        [TestMethod()]
        public void RemoveDefinition_()
        {

        }

        [TestMethod()]
        public void RemoveDefinition_1()
        {

        }
    }
}