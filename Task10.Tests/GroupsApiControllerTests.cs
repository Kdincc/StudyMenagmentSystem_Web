using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.UI.ApiControllers;

namespace Task10.Tests
{
    [TestClass]
    public class GroupsApiControllerTests
    {
        private readonly Mock<IGroupsService> _mockGroupsService;
        private readonly GroupsApiController _controller;

        public GroupsApiControllerTests()
        {
            _mockGroupsService = new Mock<IGroupsService>();
            _controller = new GroupsApiController(_mockGroupsService.Object);
        }

        [TestMethod]
        public async Task GetGroupList_ReturnsOkResult_WithGroupList()
        {
            // Arrange
            var groupList = new List<GroupDto> { new() };

            //Setup
            _mockGroupsService.Setup(service => service.GetGroupsWithCourseNamesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(groupList);

            // Act
            var result = await _controller.GetGroupList(CancellationToken.None);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.AreEqual(okResult.StatusCode, StatusCodes.Status200OK);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsNotNull(okResult);
            Assert.AreEqual(groupList, okResult.Value);
        }

        [TestMethod]
        public async Task EditGroup_ReturnsNoContent()
        {
            // Setup
            _mockGroupsService.Setup(service => service.EditGroupAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.EditGroup(1, "New Group", 2, CancellationToken.None);
            var noContent = result as NoContentResult;

            // Assert
            Assert.AreEqual(noContent.StatusCode, StatusCodes.Status204NoContent);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task CreateGroup_ReturnsCreated()
        {
            // Setup
            _mockGroupsService.Setup(service => service.CreateGroupAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateGroup("New Group", 1, CancellationToken.None);
            var created = result as CreatedAtActionResult;

            // Assert
            Assert.AreEqual(created.StatusCode, StatusCodes.Status201Created);
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task DeleteGroup_ReturnsNoContent_WhenSuccessful()
        {
            // Setup
            _mockGroupsService.Setup(service => service.DeleteGroupAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteGroup(1, CancellationToken.None);
            var noContent = result as NoContentResult;

            // Assert
            Assert.AreEqual(noContent.StatusCode, StatusCodes.Status204NoContent);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteGroup_ReturnsBadRequest_WhenUnsuccessful()
        {
            // Arrange
            _mockGroupsService.Setup(service => service.DeleteGroupAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteGroup(1, CancellationToken.None);
            var badRequestResult = result as BadRequestObjectResult;

            // Assert
            Assert.AreEqual(badRequestResult.StatusCode, StatusCodes.Status400BadRequest);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("Only groups with no students can be deleted!", badRequestResult.Value);
        }
    }
}

