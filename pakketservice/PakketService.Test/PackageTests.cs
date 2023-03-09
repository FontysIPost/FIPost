using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PakketService.Controllers;
using PakketService.Database.Converters;
using PakketService.Database.Datamodels;
using PakketService.Database.Datamodels.Dtos;
using PakketService.helpers;
using PakketService.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PakketService.Test
{
    public class Tests
    {
        private Mock<IPackageService> serviceMock;

        private PackageRequest packageRequest1;
        private PackageResponse packageResponse1;
        private PackageResponse packageResponse2;
        private readonly List<PackageResponse> packageResponses = new List<PackageResponse>();

        [SetUp]
        public void Setup()
        {
            // Instantiate mocks:
            serviceMock = new Mock<IPackageService>();

            // Create mock data:
            packageRequest1 = new PackageRequest
            {
                Sender = "Coolblue",
                ReceiverId = "1",
                Name = "Test",
                CollectionPointId = new Guid()
            };

            packageResponse1 = new PackageResponse
            {
                Id = new Guid()
            };

            packageResponse2 = new PackageResponse
            {
                Id = new Guid()
            };

            packageResponses.Clear();
            packageResponses.Add(packageResponse1);
            packageResponses.Add(packageResponse2);
        }

        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }

        [Test]
        public async Task AddPackage_Ok()
        {
            // Arrange
            serviceMock.Setup(x => x.AddAsync(packageRequest1)).ReturnsAsync(packageResponse1);
            var controller = new PackagesController(serviceMock.Object);

            // Act
            var actionResult = await controller.AddPackage(packageRequest1);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.AreEqual(packageResponse1, GetObjectResultContent(actionResult));
        }

        [Test]
        public async Task GetAllPackages_Ok()
        {
            // Arrange
            serviceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(packageResponses);
            var controller = new PackagesController(serviceMock.Object);

            // Act
            var actionResult = await controller.GetAllPackages();

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.AreEqual(packageResponses, GetObjectResultContent(actionResult));
        }

        [Test]
        public async Task GetPackageById_Ok()
        {
            // Arrange
            serviceMock.Setup(x => x.GetByIdAsync(packageResponse1.Id)).ReturnsAsync(packageResponse1);
            var controller = new PackagesController(serviceMock.Object);

            // Act
            var actionResult = await controller.GetPackageById(packageResponse1.Id);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.AreEqual(packageResponse1, GetObjectResultContent(actionResult));
        }

        [Test]
        public async Task GetPackageById_NotFound()
        {
            // Arrange
            serviceMock.Setup(x => x.GetByIdAsync(packageResponse1.Id)).Throws<NotFoundException>();
            var controller = new PackagesController(serviceMock.Object);

            // Act
            var actionResult = await controller.GetPackageById(packageResponse1.Id);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
        }

        [Test]
        public async Task UpdatePackage_Ok()
        {
            // Arrange
            serviceMock.Setup(x => x.UpdateAsync(packageResponse1.Id, packageRequest1)).ReturnsAsync(packageResponse1);
            var controller = new PackagesController(serviceMock.Object);

            // Act
            var actionResult = await controller.UpdatePackage(packageResponse1.Id, packageRequest1);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.AreEqual(packageResponse1, GetObjectResultContent(actionResult));
        }

        [Test]
        public async Task UpdatePackage_NotFound()
        {
            // Arrange
            serviceMock.Setup(x => x.UpdateAsync(packageResponse1.Id, packageRequest1)).Throws<NotFoundException>();
            var controller = new PackagesController(serviceMock.Object);

            // Act
            var actionResult = await controller.UpdatePackage(packageResponse1.Id, packageRequest1);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
        }

        [Test]
        public void Health_Ok()
        {
            // Arrange
            var controller = new PackagesController(serviceMock.Object);

            // Act
            var actionResult = controller.Health();

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkResult>(actionResult);
        }
    }
}