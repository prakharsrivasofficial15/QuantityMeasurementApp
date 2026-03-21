using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using BusinessLayer.Services;
using ModelLayer.DTOs;
using RepositoryLayer.Implementations;
using System.Linq;

namespace QuantityMeasurementApp.Tests.Integration
{
    [TestFixture]
    public class QuantityMeasurementIntegrationTests
    {
        private QuantityMeasurementService _service;
        private QuantityMeasurementDatabaseRepository _repo;

        [SetUp]
        public void Setup()
        {
            var configBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["ConnectionStrings:DefaultConnection"] = "Server=(localdb)\\MSSQLLocalDB;Database=QuantityMeasurementTestDB;Trusted_Connection=True;"
                });
            var config = configBuilder.Build();

            _repo = new QuantityMeasurementDatabaseRepository(config);
            _repo.Clear();

            _service = new QuantityMeasurementService(_repo);
        }

        [Test]
        public void Compare_ShouldPersistResultToDatabase()
        {
            // Arrange
            var req1 = new MeasurementRequest { Value = 12, Unit = "INCHES", Type = "LENGTH" };
            var req2 = new MeasurementRequest { Value = 1, Unit = "FEET", Type = "LENGTH" };

            // Act
            var record = _service.Compare(req1, req2);

            // Assert
            var saved = _repo.GetAll().FirstOrDefault();
            Assert.That(saved, Is.Not.Null);
            Assert.That(saved.Operation, Is.EqualTo("COMPARE"));
            Assert.That(saved.HasError, Is.False);
            Assert.That(saved.Result, Is.InstanceOf<MeasurementRequest>());
            var resultDto = saved.Result as MeasurementRequest;
            Assert.That(resultDto.Value, Is.EqualTo(1)); // True
        }

        [Test]
        public void Convert_ShouldPersistResultToDatabase()
        {
            // Arrange
            var req = new MeasurementRequest { Value = 12, Unit = "INCHES", Type = "LENGTH" };

            // Act
            var record = _service.Convert(req, "FEET");

            // Assert
            var saved = _repo.GetAll().FirstOrDefault();
            Assert.That(saved, Is.Not.Null);
            Assert.That(saved.Operation, Is.EqualTo("CONVERT"));
            var result = saved.Result as MeasurementRequest;
            Assert.That(result.Value, Is.EqualTo(1));
            Assert.That(result.Unit, Is.EqualTo("FEET"));
        }

        [Test]
        public void MultipleOperations_AllPersisted()
        {
            // Arrange
            var req1 = new MeasurementRequest { Value = 2, Unit = "FEET", Type = "LENGTH" };
            var req2 = new MeasurementRequest { Value = 12, Unit = "INCHES", Type = "LENGTH" };

            // Act
            _service.Add(req1, req2);
            _service.Compare(req1, req2);
            _service.Convert(req1, "INCHES");

            // Assert
            var all = _repo.GetAll().ToList();
            Assert.That(all.Count, Is.EqualTo(3));
            Assert.That(all.Count(r => r.Operation == "ADD"), Is.EqualTo(1));
            Assert.That(all.Count(r => r.Operation == "COMPARE"), Is.EqualTo(1));
            Assert.That(all.Count(r => r.Operation == "CONVERT"), Is.EqualTo(1));
        }
    }
}