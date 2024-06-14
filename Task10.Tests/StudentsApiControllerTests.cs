using Microsoft.AspNetCore.Mvc;
using Moq;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
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

            // Act
            var result = await _controller.EditStudent(1, 1, "John", "Doe", CancellationToken.None);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task CreateStudent_ReturnsCreated()
        {
            // Setup
            _mockStudentsService.Setup(service => service.CreateStudentAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateStudent("John", "Doe", 1, CancellationToken.None);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedResult));
        }

        [TestMethod]
        public async Task DeleteStudent_ReturnsNoContent()
        {
            // Setup
            _mockStudentsService.Setup(service => service.DeleteStudentAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteGroup(1, CancellationToken.None);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
    }
}

