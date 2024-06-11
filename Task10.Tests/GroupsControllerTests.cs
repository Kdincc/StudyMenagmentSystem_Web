using Microsoft.AspNetCore.Mvc;
using Moq;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.UI.Controllers;
using Task10.UI.ViewModels;

namespace Task10.Tests
{
    [TestClass]
    public class GroupsControllerTests
    {
        private Mock<IGroupsService> _groupsServiceMock;
        private readonly GroupsController _controller;

        public GroupsControllerTests()
        {
            _groupsServiceMock = new Mock<IGroupsService>();
            _controller = new GroupsController(_groupsServiceMock.Object);
        }

        [TestMethod]
        public async Task Index_ReturnsViewResult_WithGroupsListViewModel()
        {
            // Arrange
            var groups = new List<GroupDto>
            {
                new() { Name = "Group1", CourseName = "Course1" },
                new() { Name = "Group2", CourseName = "Course2" }
            };

            //Setup
            _groupsServiceMock.Setup(service => service.GetGroupsWithCourseNamesAsync(It.IsAny<CancellationToken>()))
                              .ReturnsAsync(groups);

            // Act
            var result = await _controller.Index(CancellationToken.None);
            var viewResult = result as ViewResult;
            var model = viewResult.Model as GroupsListViewModel;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsTrue(groups.SequenceEqual(model.Groups));
        }

        [TestMethod]
        public async Task Delete_Get_ReturnsViewResult_WithDeleteGroupViewModel()
        {
            // Arrange
            var deleteDto = new DeleteGroupDto { Id = 1, Name = "Group1" };
            _groupsServiceMock.Setup(service => service.GetDeleteGroupDto(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(deleteDto);

            // Act
            var result = await _controller.Delete(1, CancellationToken.None);
            var viewResult = result as ViewResult;
            var model = viewResult.Model as DeleteGroupViewModel;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(deleteDto.Name, model.Name);
            Assert.AreEqual(deleteDto.Id, model.Id);
        }

        [TestMethod]
        public async Task Delete_Get_OperationCanceledException_RedirectsToIndex()
        {
            // Arrange
            _groupsServiceMock.Setup(service => service.GetDeleteGroupDto(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                              .ThrowsAsync(new OperationCanceledException());

            // Act
            var result = await _controller.Delete(1, CancellationToken.None);
            var redirectResult = result as RedirectToActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public async Task Delete_Post_RedirectsToIndex_OnSuccess()
        {
            // Arrange
            var deleteModel = new DeleteGroupViewModel { Id = 1, Name = "Group1" };
            _groupsServiceMock.Setup(service => service.DeleteGroupAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(deleteModel, CancellationToken.None);
            var redirectResult = result as RedirectToActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public async Task Delete_Post_OperationCanceledException_RedirectsToIndex()
        {
            // Arrange
            var deleteModel = new DeleteGroupViewModel { Id = 1, Name = "Group1" };
            _groupsServiceMock.Setup(service => service.DeleteGroupAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                              .ThrowsAsync(new OperationCanceledException());

            // Act
            var result = await _controller.Delete(deleteModel, CancellationToken.None);
            var redirectResult = result as RedirectToActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public async Task Create_Get_ReturnsViewResult_WithCreateGroupViewModel()
        {
            // Arrange
            var courses = new List<CourseDto> { new() { Id = 1, Name = "Course1" } };
            _groupsServiceMock.Setup(service => service.GetCoursesAsync(It.IsAny<CancellationToken>()))
                              .ReturnsAsync(courses);

            // Act
            var result = await _controller.CreateGroup(CancellationToken.None);
            var viewResult = result as ViewResult;
            var model = viewResult.Model as CreateGroupViewModel;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsTrue(courses.SequenceEqual(model.Courses));
        }

        [TestMethod]
        public async Task Create_Get_OperationCanceledException_RedirectsToIndex()
        {
            // Arrange
            _groupsServiceMock.Setup(service => service.GetCoursesAsync(It.IsAny<CancellationToken>()))
                              .ThrowsAsync(new OperationCanceledException());

            // Act
            var result = await _controller.CreateGroup(CancellationToken.None);
            var redirectResult = result as RedirectToActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public async Task Create_Post_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var createModel = new CreateGroupViewModel
            {
                Name = "Group1",
                CourseId = 1
            };

            // Act
            var result = await _controller.CreateGroup(createModel, CancellationToken.None);
            var redirectResult = result as RedirectToActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public async Task Create_Post_InvalidModel_ReturnsViewResult()
        {
            // Arrange
            var createModel = new CreateGroupViewModel
            {
                Name = "Group1",
                CourseId = 1
            };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await _controller.CreateGroup(createModel, CancellationToken.None);
            var viewResult = result as ViewResult;
            var model = viewResult.Model as CreateGroupViewModel;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(createModel.Name, model.Name);
            Assert.AreEqual(createModel.CourseId, model.CourseId);
        }

        [TestMethod]
        public async Task Create_Post_OperationCanceledException_RedirectsToIndex()
        {
            // Arrange
            var createModel = new CreateGroupViewModel
            {
                Name = "Group1",
                CourseId = 1
            };
            _groupsServiceMock.Setup(service => service.CreateGroupAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                              .ThrowsAsync(new OperationCanceledException());

            // Act
            var result = await _controller.CreateGroup(createModel, CancellationToken.None);
            var redirectResult = result as RedirectToActionResult;


            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public async Task Edit_Get_ReturnsViewResult_WithEditGroupViewModel()
        {
            // Arrange
            var groupEditDto = new GroupEditDto
            {
                Group = new GroupDto { Id = 1, Name = "Group1", CourseId = 1 },
                Courses = [new CourseDto { Id = 1, Name = "Course1" }]
            };
            _groupsServiceMock.Setup(service => service.GetEditGroupDtoAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(groupEditDto);

            var result = await _controller.EditGroup(1, CancellationToken.None);
            var viewResult = result as ViewResult;
            var model = viewResult.Model as EditGroupViewModel;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(groupEditDto.Group.Name, model.Name);
            Assert.AreEqual(groupEditDto.Group.CourseId, model.CourseId);
            Assert.AreEqual(groupEditDto.Group.Id, model.Id);
            Assert.IsTrue(groupEditDto.Courses.SequenceEqual(model.Courses));
        }

        [TestMethod]
        public async Task Edit_Get_OperationCanceledException_RedirectsToIndex()
        {
            // Arrange
            _groupsServiceMock.Setup(service => service.GetEditGroupDtoAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                              .ThrowsAsync(new OperationCanceledException());

            // Act
            var result = await _controller.EditGroup(1, CancellationToken.None);
            var redirectResult = result as RedirectToActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public async Task Edit_Post_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var editModel = new EditGroupViewModel
            {
                Name = "Group1",
                Id = 1,
                CourseId = 1
            };

            // Act
            var result = await _controller.EditGroup(editModel, CancellationToken.None);
            var redirectResult = result as RedirectToActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public async Task Edit_Post_InvalidModel_ReturnsViewResult()
        {
            // Arrange
            var editModel = new EditGroupViewModel
            {
                Name = "Group1",
                Id = 1,
                CourseId = 1
            };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await _controller.EditGroup(editModel, CancellationToken.None);
            var viewResult = result as ViewResult;
            var model = viewResult.Model as EditGroupViewModel;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(editModel.Name, model.Name);
            Assert.AreEqual(editModel.CourseId, model.CourseId);
            Assert.AreEqual(editModel.Id, model.Id);
        }

        [TestMethod]
        public async Task Edit_Post_OperationCanceledException_RedirectsToIndex()
        {
            // Arrange
            var editModel = new EditGroupViewModel
            {
                Name = "Group1",
                Id = 1,
                CourseId = 1
            };

            //Setup
            _groupsServiceMock.Setup(service => service.EditGroupAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                              .ThrowsAsync(new OperationCanceledException());

            // Act
            var result = await _controller.EditGroup(editModel, CancellationToken.None);
            var redirectResult = result as RedirectToActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", redirectResult.ActionName);
        }
    }
}
