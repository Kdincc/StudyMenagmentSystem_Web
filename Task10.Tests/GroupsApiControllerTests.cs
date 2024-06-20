using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.Test.Core.Models;
using Task10.UI.ApiControllers;
using Task10.UI.Controllers;
using Task10.UI.ViewModels;

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
            // Arrange
            string groupName = "New Group";
            int courseId = 2;
            int groupId = 1;
            var viewModel = new EditGroupViewModel()
            {
                Name = groupName,
                CourseId = courseId,
                Id = groupId
            };

            // Setup
            _mockGroupsService.Setup(service => service.EditGroupAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _mockGroupsService.Setup(service => service.IsCourseExistsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(true);
            _mockGroupsService.Setup(service => service.IsGroupExistsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(true);

            // Act
            var result = await _controller.EditGroup(viewModel, CancellationToken.None);
            var noContent = result as NoContentResult;

            // Assert
            Assert.AreEqual(noContent.StatusCode, StatusCodes.Status204NoContent);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task CreateGroup_ReturnsCreated()
        {
            // Arrange
            string groupName = "New Group";
            int courseId = 1;
            var viewModel = new CreateGroupViewModel()
            {
                Name = groupName,
                CourseId = courseId,
            };

            // Setup
            _mockGroupsService.Setup(service => service.CreateGroupAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _mockGroupsService.Setup(service => service.IsCourseExistsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(true);

            // Act
            var result = await _controller.CreateGroup(viewModel, CancellationToken.None);
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
            _mockGroupsService.Setup(service => service.IsGroupExistsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
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
            _mockGroupsService.Setup(service => service.IsGroupExistsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteGroup(1, CancellationToken.None);
            var badRequestResult = result as BadRequestObjectResult;

            // Assert
            Assert.AreEqual(badRequestResult.StatusCode, StatusCodes.Status400BadRequest);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("Only groups with no students can be deleted!", badRequestResult.Value);
        }

        [TestMethod]
        public async Task DeleteGroup_GroupNotExists_ReturnsNotFound()
        {
            // Arrange
            var groupId = 1;
            var cancellationToken = CancellationToken.None;

            // Setup
            _mockGroupsService
                .Setup(service => service.IsGroupExistsAsync(groupId, cancellationToken))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteGroup(groupId, cancellationToken);
            var notFoundResult = result as NotFoundObjectResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [TestMethod]
        public async Task CreateGroup_CourseNotExists_ReturnsNotFound()
        {
            // Arrange
            var groupName = "TestGroup";
            var courseId = 1;
            var cancellationToken = CancellationToken.None;
            var viewModel = new CreateGroupViewModel()
            {
                Name = groupName,
                CourseId = courseId,
            };

            // Setup
            _mockGroupsService
                .Setup(service => service.IsCourseExistsAsync(courseId, cancellationToken))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.CreateGroup(viewModel, cancellationToken);
            var notFoundResult = result as NotFoundObjectResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [TestMethod]
        public async Task EditGroup_GroupNotExists_ReturnsNotFound()
        {
            // Arrange
            var groupId = 1;
            var groupName = "TestGroup";
            var courseId = 1;
            var cancellationToken = CancellationToken.None;
            var viewModel = new EditGroupViewModel()
            {
                Id = groupId,
                Name = groupName,
                CourseId = courseId,
            };

            //Setup
            _mockGroupsService
                .Setup(service => service.IsCourseExistsAsync(courseId, cancellationToken))
                .ReturnsAsync(true);
            _mockGroupsService
                .Setup(service => service.IsGroupExistsAsync(groupId, cancellationToken))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.EditGroup(viewModel, cancellationToken);
            var notFoundResult = result as NotFoundObjectResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [TestMethod]
        public async Task EditGroup_CourseNotExists_ReturnsNotFound()
        {
            // Arrange
            var groupId = 1;
            var groupName = "TestGroup";
            var courseId = 1;
            var cancellationToken = CancellationToken.None;
            var viewModel = new EditGroupViewModel()
            {
                Id = groupId,
                Name = groupName,
                CourseId = courseId,
            };

            //Setup
            _mockGroupsService
                .Setup(service => service.IsCourseExistsAsync(courseId, cancellationToken))
                .ReturnsAsync(false);
            _mockGroupsService
                .Setup(service => service.IsGroupExistsAsync(groupId, cancellationToken))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.EditGroup(viewModel, cancellationToken);
            var notFoundResult = result as NotFoundObjectResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }
    }
}

