using LocatieService.Controllers;
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
    public class RoomTests
    {
        private Mock<IRoomService> serviceMock;

        private RoomRequest roomRequest1;
        private RoomResponse roomResponse1;
        private RoomResponse roomResponse2;
        private readonly List<RoomResponse> roomResponses = new List<RoomResponse>();

        [SetUp]
        public void Setup()
        {
            // Instantiate mocks:
            serviceMock = new Mock<IRoomService>();

            // Create mock data:

            roomRequest1 = new RoomRequest
            {
                Name = "Room_1"
            };

            roomResponse1 = new RoomResponse
            {
                Id = new Guid(),
                Name = "Room_1"
            };

            roomResponse2 = new RoomResponse
            {
                Id = new Guid(),
                Name = "Room_2"
            };

            roomResponses.Clear();
            roomResponses.Add(roomResponse1);
            roomResponses.Add(roomResponse2);
        }

        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }

        [Test]
        public async Task AddRoom_Ok()
        {
            // Arrange
            serviceMock.Setup(x => x.AddAsync(roomRequest1)).ReturnsAsync(roomResponse1);
            var controller = new RoomController(serviceMock.Object);

            // Act
            var actionResult = await controller.AddRoom(roomRequest1);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.AreEqual(roomResponse1, GetObjectResultContent(actionResult));
        }

        [Test]
        public async Task AddRoom_Conflict()
        {
            // Arrange
            serviceMock.Setup(x => x.AddAsync(roomRequest1)).Throws<DuplicateException>();
            var controller = new RoomController(serviceMock.Object);

            // Act
            var actionResult = await controller.AddRoom(roomRequest1);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<ConflictObjectResult>(actionResult.Result);
        }

        [Test]
        public async Task GetAllRooms_Ok()
        {
            // Arrange
            serviceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(roomResponses);
            var controller = new RoomController(serviceMock.Object);

            // Act
            var actionResult = await controller.GetAllRooms();

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.AreEqual(roomResponses, GetObjectResultContent(actionResult));
        }

        [Test]
        public async Task GetRoomById_Ok()
        {
            // Arrange
            serviceMock.Setup(x => x.GetByIdAsync(roomResponse1.Id)).ReturnsAsync(roomResponse1);
            var controller = new RoomController(serviceMock.Object);

            // Act
            var actionResult = await controller.GetRoomById(roomResponse1.Id);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.AreEqual(roomResponse1, GetObjectResultContent(actionResult));
        }

        [Test]
        public async Task GetRoomById_NotFound()
        {
            // Arrange
            serviceMock.Setup(x => x.GetByIdAsync(roomResponse1.Id)).Throws<NotFoundException>();
            var controller = new RoomController(serviceMock.Object);

            // Act
            var actionResult = await controller.GetRoomById(roomResponse1.Id);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
        }

        [Test]
        public async Task GetRoomByName_Ok()
        {
            // Arrange
            serviceMock.Setup(x => x.GetByNameAsync(roomResponse1.Name)).ReturnsAsync(roomResponse1);
            var controller = new RoomController(serviceMock.Object);

            // Act
            var actionResult = await controller.GetRoomByName(roomResponse1.Name);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.AreEqual(roomResponse1, GetObjectResultContent(actionResult));
        }

        [Test]
        public async Task GetRoomByName_NotFound()
        {
            // Arrange
            serviceMock.Setup(x => x.GetByNameAsync(roomResponse1.Name)).Throws<NotFoundException>();
            var controller = new RoomController(serviceMock.Object);

            // Act
            var actionResult = await controller.GetRoomByName(roomResponse1.Name);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
        }

        [Test]
        public async Task UpdateRoom_Ok()
        {
            // Arrange
            serviceMock.Setup(x => x.UpdateAsync(new Guid(), roomRequest1)).ReturnsAsync(roomResponse1);
            var controller = new RoomController(serviceMock.Object);

            // Act
            var actionResult = await controller.UpdateRoom(new Guid(), roomRequest1);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.AreEqual(roomResponse1, GetObjectResultContent(actionResult));
        }

        [Test]
        public async Task UpdateRoom_Conflict()
        {
            // Arrange
            serviceMock.Setup(x => x.UpdateAsync(new Guid(), roomRequest1)).Throws<DuplicateException>();
            var controller = new RoomController(serviceMock.Object);

            // Act
            var actionResult = await controller.UpdateRoom(new Guid(), roomRequest1);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<ConflictObjectResult>(actionResult.Result);
        }

        [Test]
        public async Task UpdateRoom_NotFound()
        {
            // Arrange
            serviceMock.Setup(x => x.UpdateAsync(new Guid(), roomRequest1)).Throws<NotFoundException>();
            var controller = new RoomController(serviceMock.Object);

            // Act
            var actionResult = await controller.UpdateRoom(new Guid(), roomRequest1);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
        }

        [Test]
        public async Task DeleteRoomById_NoContent()
        {
            // Arrange
            serviceMock.Setup(x => x.DeleteRoomAsync(roomResponse1.Id)).ReturnsAsync(new RoomResponse());
            var controller = new RoomController(serviceMock.Object);

            // Act
            var actionResult = await controller.DeleteRoomById(roomResponse1.Id);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<NoContentResult>(actionResult.Result);
        }

        [Test]
        public async Task DeleteRoomById_NotFound()
        {
            // Arrange
            serviceMock.Setup(x => x.DeleteRoomAsync(roomResponse1.Id)).Throws<NotFoundException>();
            var controller = new RoomController(serviceMock.Object);

            // Act
            var actionResult = await controller.DeleteRoomById(roomResponse1.Id);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
        }
    }
}