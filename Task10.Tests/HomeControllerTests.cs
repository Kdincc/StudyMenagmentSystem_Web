using Microsoft.AspNetCore.Mvc;
using Moq;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.UI.Controllers;
using Task10.UI.ViewModels;

namespace Task10.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        private readonly HomeController _controller;
        private readonly Mock<IHomeService> _homeServiceMock = new();

        public HomeControllerTests()
        {
            _controller = new(_homeServiceMock.Object);
        }

        [TestMethod]
        public async Task Index_ReturnsViewResult_WithHomeViewModel()
        {
            // Arrange
            var homeDto = new HomeDto();

            //Setup
            _homeServiceMock.Setup(service => service.GetHomeDtoAsync(It.IsAny<CancellationToken>()))
                            .ReturnsAsync(homeDto);

            // Act
            var result = await _controller.Index(CancellationToken.None) as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(result.Model, typeof(HomeViewModel));
        }

        [TestMethod]
        public void Privacy_ReturnsViewResult()
        {
            // Act
            var result = _controller.Privacy();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
