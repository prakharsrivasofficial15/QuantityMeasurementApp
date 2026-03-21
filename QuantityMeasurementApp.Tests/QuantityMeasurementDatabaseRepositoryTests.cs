using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using RepositoryLayer.Implementations;
using ModelLayer.DTOs;
using System;
using System.Linq;

namespace QuantityMeasurementApp.Tests.Repository
{
    [TestFixture]
    public class QuantityMeasurementDatabaseRepositoryTests
    {
        private IConfiguration _config;
        private QuantityMeasurementDatabaseRepository _repo;

        [SetUp]
        public void Setup()
        {
            // Use a test database connection (could be a separate test DB)
            var configBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["ConnectionStrings:DefaultConnection"] = "Server=(localdb)\\MSSQLLocalDB;Database=QuantityMeasurementTestDB;Trusted_Connection=True;"
                });
            _config = configBuilder.Build();

            // Ensure test DB exists and is clean (you'd run schema script once manually)
            _repo = new QuantityMeasurementDatabaseRepository(_config);
            _repo.Clear(); // Start fresh
        }

        [TearDown]
        public void TearDown()
        {
            _repo.Clear(); // Clean up after tests
        }

        [Test]
        public void Save_ShouldStoreRecordInDatabase()
        {
            // Arrange
            var request = new MeasurementRequest { Value = 12, Unit = "INCHES", Type = "LENGTH" };
            var result = new MeasurementRequest { Value = 1, Unit = "FEET", Type = "LENGTH" };
            var record = new MeasurementRecord("CONVERT", request, result);

            // Act
            _repo.Save(record);

            // Assert
            var all = _repo.GetAll().ToList();
            Assert.That(all.Count, Is.EqualTo(1));
            var saved = all.First();
            Assert.That(saved.Operation, Is.EqualTo("CONVERT"));
            Assert.That(saved.Operand1.Value, Is.EqualTo(12));
            Assert.That(saved.Result, Is.InstanceOf<MeasurementRequest>());
        }

        [Test]
        public void GetAll_ShouldReturnAllSavedRecords()
        {
            // Arrange
            var req1 = new MeasurementRequest { Value = 1, Unit = "FEET", Type = "LENGTH" };
            var req2 = new MeasurementRequest { Value = 12, Unit = "INCHES", Type = "LENGTH" };
            _repo.Save(new MeasurementRecord("COMPARE", req1, req2, 
                new MeasurementRequest { Value = 1, Unit = "BOOLEAN", Type = "RESULT" }));
            _repo.Save(new MeasurementRecord("ADD", req1, req2, 
                new MeasurementRequest { Value = 2, Unit = "FEET", Type = "LENGTH" }));

            // Act
            var all = _repo.GetAll().ToList();

            // Assert
            Assert.That(all.Count, Is.EqualTo(2));
        }

        [Test]
        public void Clear_ShouldDeleteAllRecords()
        {
            // Arrange
            var request = new MeasurementRequest { Value = 10, Unit = "FEET", Type = "LENGTH" };
            _repo.Save(new MeasurementRecord("CONVERT", request, new MeasurementRequest { Value = 120, Unit = "INCHES", Type = "LENGTH" }));

            // Act
            _repo.Clear();

            // Assert
            Assert.That(_repo.GetAll().Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetTotalCount_ShouldReturnCorrectNumber()
        {
            // Arrange
            var request = new MeasurementRequest { Value = 1, Unit = "FEET", Type = "LENGTH" };
            _repo.Save(new MeasurementRecord("CONVERT", request, request));

            // Act
            var count = _repo.GetTotalCount();

            // Assert
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void GetByOperation_ShouldFilterCorrectly()
        {
            // Arrange
            var req = new MeasurementRequest { Value = 5, Unit = "FEET", Type = "LENGTH" };
            _repo.Save(new MeasurementRecord("CONVERT", req, req));
            _repo.Save(new MeasurementRecord("COMPARE", req, req, 
                new MeasurementRequest { Value = 1, Unit = "BOOLEAN", Type = "RESULT" }));

            // Act
            var converts = _repo.GetByOperation("CONVERT").ToList();

            // Assert
            Assert.That(converts.Count, Is.EqualTo(1));
            Assert.That(converts.First().Operation, Is.EqualTo("CONVERT"));
        }
    }
}