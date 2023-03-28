using LocatieService.Controllers;
using LocatieService.Database.Datamodels;
using LocatieService.Database.Datamodels.Dtos;
using LocatieService.Helpers;
using LocatieService.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocatieService.Test
{
    [TestFixture]
    public class CityTests
    {
        private Mock<ICityService> serviceMock;

        private CityRequest cityRequest1;
        private CityResponse cityResponse1;
        private CityResponse cityResponse2;
        private readonly List<CityResponse> cityResponses = new List<CityResponse>();

        [SetUp]
        public void Setup()
        {
            // Instantiate mocks:
            serviceMock = new Mock<ICityService>();

            // Create mock data:

            cityRequest1 = new CityRequest
            {
                Name = "City_1"
            };

            cityResponse1 = new CityResponse
            {
                Id = new Guid(),
                Name = "City_1"
            };

            cityResponse2 = new CityResponse
            {
                Id = new Guid(),
                Name = "City_2"
            };

            cityResponses.Clear();
            cityResponses.Add(cityResponse1);
            cityResponses.Add(cityResponse2);
        }

        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }

        [Test]
        public async Task AddCity_Ok()
        {
            // Arrange
            serviceMock.Setup(x => x.AddAsync(cityRequest1)).ReturnsAsync(cityResponse1);
            var controller = new CityController(serviceMock.Object);

            // Act
            var actionResult = await controller.AddCity(cityRequest1);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.AreEqual(cityResponse1, GetObjectResultContent(actionResult));
        }

        [Test]
        public async Task AddCity_Conflict()
        {
            // Arrange
            serviceMock.Setup(x => x.AddAsync(cityRequest1)).Throws<DuplicateException>();
            var controller = new CityController(serviceMock.Object);

            // Act
            var actionResult = await controller.AddCity(cityRequest1);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<ConflictObjectResult>(actionResult.Result);
        }

        [Test]
        public async Task GetAllCities_Ok()
        {
            // Arrange
            serviceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(cityResponses);
            var controller = new CityController(serviceMock.Object);

            // Act
            var actionResult = await controller.GetAllCities();

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.AreEqual(cityResponses, GetObjectResultContent(actionResult));
        }

        [Test]
        public async Task GetCityById_Ok()
        {
            // Arrange
            serviceMock.Setup(x => x.GetByIdAsync(cityResponse1.Id)).ReturnsAsync(cityResponse1);
            var controller = new CityController(serviceMock.Object);

            // Act
            var actionResult = await controller.GetCityById(cityResponse1.Id);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.AreEqual(cityResponse1, GetObjectResultContent(actionResult));
        }

        [Test]
        public async Task GetCityById_NotFound()
        {
            // Arrange
            serviceMock.Setup(x => x.GetByIdAsync(cityResponse1.Id)).Throws<NotFoundException>();
            var controller = new CityController(serviceMock.Object);

            // Act
            var actionResult = await controller.GetCityById(cityResponse1.Id);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
        }

        [Test]
        public async Task GetCityByName_Ok()
        {
            // Arrange
            serviceMock.Setup(x => x.GetByNameAsync(cityResponse1.Name)).ReturnsAsync(cityResponse1);
            var controller = new CityController(serviceMock.Object);

            // Act
            var actionResult = await controller.GetCityByName(cityResponse1.Name);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.AreEqual(cityResponse1, GetObjectResultContent(actionResult));
        }

        [Test]
        public async Task GetCityByName_NotFound()
        {
            // Arrange
            serviceMock.Setup(x => x.GetByNameAsync(cityResponse1.Name)).Throws<NotFoundException>();
            var controller = new CityController(serviceMock.Object);

            // Act
            var actionResult = await controller.GetCityByName(cityResponse1.Name);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
        }

        [Test]
        public async Task UpdateCity_Ok()
        {
            // Arrange
            serviceMock.Setup(x => x.UpdateAsync(new Guid(), cityRequest1)).ReturnsAsync(cityResponse1);
            var controller = new CityController(serviceMock.Object);

            // Act
            var actionResult = await controller.UpdateCity(new Guid(), cityRequest1);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.AreEqual(cityResponse1, GetObjectResultContent(actionResult));
        }

        [Test]
        public async Task UpdateCity_Conflict()
        {
            // Arrange
            serviceMock.Setup(x => x.UpdateAsync(new Guid(), cityRequest1)).Throws<DuplicateException>();
            var controller = new CityController(serviceMock.Object);

            // Act
            var actionResult = await controller.UpdateCity(new Guid(), cityRequest1);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<ConflictObjectResult>(actionResult.Result);
        }

        [Test]
        public async Task UpdateCity_NotFound()
        {
            // Arrange
            serviceMock.Setup(x => x.UpdateAsync(new Guid(), cityRequest1)).Throws<NotFoundException>();
            var controller = new CityController(serviceMock.Object);

            // Act
            var actionResult = await controller.UpdateCity(new Guid(), cityRequest1);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
        }

        [Test]
        public async Task DeleteCityById_NoContent()
        {
            // Arrange
            serviceMock.Setup(x => x.DeleteAsync(cityResponse1.Id)).ReturnsAsync(new CityResponse());
            var controller = new CityController(serviceMock.Object);

            // Act
            var actionResult = await controller.DeleteCityById(cityResponse1.Id);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<NoContentResult>(actionResult.Result);
        }

        [Test]
        public async Task DeleteCityById_NotFound()
        {
            // Arrange
            serviceMock.Setup(x => x.DeleteAsync(cityResponse1.Id)).Throws<NotFoundException>();
            var controller = new CityController(serviceMock.Object);

            // Act
            var actionResult = await controller.DeleteCityById(cityResponse1.Id);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
        }
    }
}