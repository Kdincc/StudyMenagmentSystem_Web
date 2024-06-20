using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.Test.Core.Models;
using Task10.UI.ApiControllers;

namespace MyProject.Tests
{
    [TestClass]
    public class StudentsApiControllerTests
    {
        private readonly Mock<IStudentsService> _mockStudentsService;
        private readonly StudentsApiController _controller;

        public StudentsApiControllerTests()
        {
            _mockStudentsService = new Mock<IStudentsService>();
            _controller = new StudentsApiController(_mockStudentsService.Object);
        }

        [TestMethod]
        public async Task GetStudents_ReturnsOkResult_WithStudentList()
        {
            // Arrange
            var studentList = new List<StudentDto> { new() };

            //Setup
            _mockStudentsService.Setup(service => service.GetStudentsWithGroupsNameAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(studentList);

            // Act
            var result = await _controller.GetStudents(CancellationToken.None);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.AreEqual(okResult.StatusCode, StatusCodes.Status200OK);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsNotNull(okResult);
            Assert.AreEqual(studentList, okResult.Value);
        }

        [TestMethod]
        public async Task EditStudent_ReturnsNoContent()
        {
            // Setup
            _mockStudentsService.Setup(service => service.EditStudentAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _mockStudentsService.Setup(service => service.IsStudentExistsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            _mockStudentsService.Setup(service => service.IsGroupExistsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.EditStudent(1, 1, "John", "Doe", CancellationToken.None);
            var noContent = result as NoContentResult;

            // Assert
            Assert.AreEqual(noContent.StatusCode, StatusCodes.Status204NoContent);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task CreateStudent_ReturnsCreated()
        {
            // Setup
            _mockStudentsService.Setup(service => service.CreateStudentAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _mockStudentsService.Setup(service => service.IsGroupExistsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.CreateStudent("John", "Doe", 1, CancellationToken.None);
            var created = result as CreatedAtActionResult;

            // Assert
            Assert.AreEqual(created.StatusCode, StatusCodes.Status201Created);
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task DeleteStudent_ReturnsNoContent()
        {
            // Setup
            _mockStudentsService.Setup(service => service.DeleteStudentAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _mockStudentsService.Setup(service => service.IsStudentExistsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteGroup(1, CancellationToken.None);
            var noContent = result as NoContentResult;

            // Assert
            Assert.AreEqual(noContent.StatusCode, StatusCodes.Status204NoContent);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task EditStudent_StudentNotExists_ReturnsNotFound()
        {
            // Arrange
            var studentId = 1;
            var groupId = 1;
            var name = "John";
            var lastName = "Doe";
            var cancellationToken = CancellationToken.None;

            var mockStudentsService = new Mock<IStudentsService>();
            _mockStudentsService
                .Setup(service => service.IsStudentExistsAsync(studentId, cancellationToken))
                .ReturnsAsync(false);
            _mockStudentsService
                .Setup(service => service.IsGroupExistsAsync(groupId, cancellationToken))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.EditStudent(studentId, groupId, name, lastName, cancellationToken);
            var notFoundResult = result as NotFoundObjectResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [TestMethod]
        public async Task EditStudent_GroupNotExists_ReturnsNotFound()
        {
            // Arrange
            var studentId = 1;
            var groupId = 1;
            var name = "John";
            var lastName = "Doe";
            var cancellationToken = CancellationToken.None;

            _mockStudentsService
                .Setup(service => service.IsStudentExistsAsync(studentId, cancellationToken))
                .ReturnsAsync(true);
            _mockStudentsService
                .Setup(service => service.IsGroupExistsAsync(groupId, cancellationToken))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.EditStudent(studentId, groupId, name, lastName, cancellationToken);
            var notFoundResult = result as NotFoundObjectResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [TestMethod]
        public async Task CreateStudent_GroupNotExists_ReturnsNotFound()
        {
            // Arrange
            var name = "John";
            var lastName = "Doe";
            var groupId = 1;
            var cancellationToken = CancellationToken.None;

            _mockStudentsService
                .Setup(service => service.IsGroupExistsAsync(groupId, cancellationToken))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.CreateStudent(name, lastName, groupId, cancellationToken);
            var notFoundResult = result as NotFoundObjectResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [TestMethod]
        public async Task DeleteStudent_StudentNotExists_ReturnsNotFound()
        {
            // Arrange
            var studentId = 1;
            var cancellationToken = CancellationToken.None;

            _mockStudentsService
                .Setup(service => service.IsStudentExistsAsync(studentId, cancellationToken))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteGroup(studentId, cancellationToken);
            var notFoundResult = result as NotFoundObjectResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }
    }
}

