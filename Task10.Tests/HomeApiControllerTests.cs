using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Task10.Core.Aggregates;
using Task10.Core.Interfaces;
using Task10.UI.ApiControllers;

namespace Task10.Tests
{
    [TestClass]
    public class HomeApiControllerTests
    {
        private readonly Mock<IHomeService> _mockHomeService;
        private readonly HomeApiController _controller;

        public HomeApiControllerTests()
        {
            _mockHomeService = new Mock<IHomeService>();
            _controller = new HomeApiController(_mockHomeService.Object);
        }

        [TestMethod]
        public async Task GetCourses_ReturnsOkResult_WithCourseList()
        {
            // Arrange
            var courseList = new СourseList([]);

            //Setup
            _mockHomeService.Setup(service => service.GetCourseListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(courseList);

            // Act
            var result = await _controller.GetCourses(CancellationToken.None);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.AreEqual(okResult.StatusCode, StatusCodes.Status200OK);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsNotNull(okResult);
            Assert.AreEqual(courseList, okResult.Value);
        }
    }
}
