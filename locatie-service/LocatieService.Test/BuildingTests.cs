using LocatieService.Controllers;
using LocatieService.Database.Datamodels;
using LocatieService.Database.Datamodels.Dtos;
using LocatieService.Database.Datamodels.Dtos.Responses;
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
    public class BuildingTests
    {
        private Mock<IBuildingService> serviceMock;

        private BuildingRequest buildingRequest1;
        private BuildingRequest buildingRequest2;
        private BuildingResponse buildingResponse1;
        private BuildingResponse buildingResponse2;
        private readonly List<BuildingResponse> buildingResponses = new List<BuildingResponse>();

        [SetUp]
        public void Setup()
        {
            serviceMock = new Mock<IBuildingService>();

            buildingRequest1 = new BuildingRequest
            {
                Name = "Building_1"
            };

            buildingRequest2 = new BuildingRequest
            {
                Name = "Building_2"
            };

            buildingResponse1 = new BuildingResponse
            {
                Id = new Guid(),
                Name = "Building_1"
            };

            buildingResponse2 = new BuildingResponse
            {
                Id = new Guid(),
                Name = "Building_2"
            };

            buildingResponses.Clear();
            buildingResponses.Add(buildingResponse1);
            buildingResponses.Add(buildingResponse2);

        }

        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }

        [Test]
        public async Task AddBuilding_Ok()
        {
            serviceMock.Setup(x => x.AddAsync(buildingRequest1)).ReturnsAsync(buildingResponse1);
            var controller = new BuildingController(serviceMock.Object);
            var actionResult = await controller.AddBuilding(buildingRequest1);
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.AreEqual(buildingResponse1, GetObjectResultContent(actionResult));
        }

        [Test]
        public async Task AddBuilding_Conflict()
        {
            serviceMock.Setup(x => x.AddAsync(buildingRequest1)).Throws<DuplicateException>();
            var controller = new BuildingController(serviceMock.Object);
            var actionResult = await controller.AddBuilding(buildingRequest1);
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<ConflictObjectResult>(actionResult.Result);
        }

        [Test]
        public async Task GetAllBuildings_Ok()
        {
            serviceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(buildingResponses);
            var controller = new BuildingController(serviceMock.Object);
            var actionResult = await controller.GetAllBuildings();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.AreEqual(buildingResponses, GetObjectResultContent(actionResult));
        }

        [Test]
        public async Task GetBuildingById_Ok()
        {
            serviceMock.Setup(x => x.GetByIdAsync(buildingResponse1.Id)).ReturnsAsync(buildingResponse1);
            var controller = new BuildingController(serviceMock.Object);
            var actionResult = await controller.GetBuildingById(buildingResponse1.Id);
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.AreEqual(buildingResponse1, GetObjectResultContent(actionResult));
        }

        [Test]
        public async Task GetBuildingById_NotFound()
        {
            serviceMock.Setup(x => x.GetByIdAsync(buildingResponse1.Id)).Throws<NotFoundException>();
            var controller = new BuildingController(serviceMock.Object);
            var actionResult = await controller.GetBuildingById(buildingResponse1.Id);
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
        }

        [Test]
        public async Task GetBuildingByName_Ok()
        {
            serviceMock.Setup(x => x.GetByNameAsync(buildingRequest1.Name)).ReturnsAsync(buildingResponse1);
            var controller = new BuildingController(serviceMock.Object);
            var actionResult = await controller.GetBuildingByName(buildingRequest1.Name);
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.AreEqual(buildingResponse1, GetObjectResultContent(actionResult));
        }

        [Test]
        public async Task GetBuildingByName_NotFound()
        {
            serviceMock.Setup(x => x.GetByNameAsync(buildingRequest1.Name)).Throws<NotFoundException>();
            var controller = new BuildingController(serviceMock.Object);
            var actionResult = await controller.GetBuildingByName(buildingRequest1.Name);
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
        }

        [Test]
        public async Task UpdateBuilding_Ok()
        {
            serviceMock.Setup(x => x.UpdateAsync(new Guid(), buildingRequest1)).ReturnsAsync(buildingResponse1);
            var controller = new BuildingController(serviceMock.Object);
            var actionResult = await controller.UpdateBuilding(new Guid(), buildingRequest1);
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.AreEqual(buildingResponse1, GetObjectResultContent(actionResult));
        }

        [Test]
        public async Task UpdateBuilding_Duplicate()
        {
            serviceMock.Setup(x => x.UpdateAsync(new Guid(), buildingRequest1)).Throws<DuplicateException>();
            var controller = new BuildingController(serviceMock.Object);
            var actionResult = await controller.UpdateBuilding(new Guid(), buildingRequest1);
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<ConflictObjectResult>(actionResult.Result);
        }

        [Test]
        public async Task UpdateBuilding_NotFound()
        {
            serviceMock.Setup(x => x.UpdateAsync(new Guid(), buildingRequest1)).Throws<NotFoundException>();
            var controller = new BuildingController(serviceMock.Object);
            var actionResult = await controller.UpdateBuilding(new Guid(), buildingRequest1);
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
        }

        [Test]
        public async Task DeleteBuilding_NoContent()
        {
            serviceMock.Setup(x => x.DeleteAsync(new Guid())).ReturnsAsync(new BuildingResponse());
            var controller = new BuildingController(serviceMock.Object);
            var actionResult = await controller.DeleteBuildingById(new Guid());
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<NoContentResult>(actionResult.Result);
        }

        [Test]
        public async Task DeleteBuilding_NotFound()
        {
            serviceMock.Setup(x => x.DeleteAsync(new Guid())).Throws<NotFoundException>();
            var controller = new BuildingController(serviceMock.Object);
            var actionResult = await controller.DeleteBuildingById(new Guid());
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
        }
    }
}