using Microsoft.AspNetCore.Mvc;
using Moq;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.UI.Controllers;
using Task10.UI.ViewModels;

namespace Task10.Tests
{
    [TestClass]
    public class StudentsControllerTests
    {
        private readonly Mock<IStudentsService> _studentsServiceMock;
        private readonly StudentsController _controller;

        public StudentsControllerTests()
        {
            _studentsServiceMock = new Mock<IStudentsService>();
            _controller = new StudentsController(_studentsServiceMock.Object);
        }

        [TestMethod]
        public async Task Index_ReturnsViewResult_WithStudentListViewModel()
        {
            //Arrange
            var students = new List<StudentDto>
            {
                new() { Name = "John", LastName = "Doe", GroupName = "Group1" },
                new() { Name = "Jane", LastName = "Smith", GroupName = "Group2" }
            };

            //Setup
            _studentsServiceMock.Setup(service => service.GetStudentsWithGroupsNameAsync(It.IsAny<CancellationToken>()))
                                .ReturnsAsync(students);

            //Act
            var result = await _controller.Index(CancellationToken.None);
            var viewResult = result as ViewResult;
            var model = viewResult.Model as StudentListViewModel;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsTrue(students.SequenceEqual(model.Students));
        }

        [TestMethod]
        public async Task Edit_Get_ReturnsViewResult_WithEditStudentViewModel()
        {
            //Arrange
            var studentEditDto = new StudentEditDto
            {
                Student = new StudentDto { Id = 1, Name = "John", LastName = "Doe", GroupId = 1 },
                Groups = new List<GroupDto> { new GroupDto { Id = 1, Name = "Group1" } }
            };

            //Setup
            _studentsServiceMock.Setup(service => service.GetEditStudentDtoAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                                .ReturnsAsync(studentEditDto);

            //Act
            var result = await _controller.Edit(1, CancellationToken.None);
            var viewResult = result as ViewResult;
            var model = viewResult.Model as EditStudentViewModel;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(studentEditDto.Student.Name, model.Name);
            Assert.AreEqual(studentEditDto.Student.LastName, model.LastName);
            Assert.AreEqual(studentEditDto.Student.GroupId, model.GroupId);
            Assert.IsTrue(studentEditDto.Groups.SequenceEqual(model.Groups));
        }

        [TestMethod]
        public async Task Edit_Post_ValidModel_RedirectsToIndex()
        {
            //Arrange
            var editModel = new EditStudentViewModel
            {
                Id = 1,
                Name = "John",
                LastName = "Doe",
                GroupId = 1
            };

            //Act
            var result = await _controller.Edit(editModel, CancellationToken.None);
            var redirectResult = result as RedirectToActionResult;

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public async Task Edit_Post_InvalidModel_ReturnsViewResult()
        {
            //Arrange
            var editModel = new EditStudentViewModel
            {
                Id = 1,
                Name = "John",
                LastName = "Doe",
                GroupId = 1
            };

            //Setup
            _controller.ModelState.AddModelError("Name", "Required");

            //Act
            var result = await _controller.Edit(editModel, CancellationToken.None);
            var viewResult = result as ViewResult;
            var model = viewResult.Model as EditStudentViewModel;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(editModel.Name, model.Name);
            Assert.AreEqual(editModel.LastName, model.LastName);
            Assert.AreEqual(editModel.GroupId, model.GroupId);
        }

        [TestMethod]
        public async Task Delete_Get_ReturnsViewResult_WithDeleteStudentViewModel()
        {
            //Arrange
            var deleteDto = new DeleteStudentDto { Id = 1, Name = "John", LastName = "Doe" };

            //Setup
            _studentsServiceMock.Setup(service => service.GetDeleteStudentDto(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                                .ReturnsAsync(deleteDto);

            //Act
            var result = await _controller.Delete(1, CancellationToken.None);
            var viewResult = result as ViewResult;
            var model = viewResult.Model as DeleteStudentViewModel;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(deleteDto.Name, model.Name);
            Assert.AreEqual(deleteDto.LastName, model.LastName);
            Assert.AreEqual(deleteDto.Id, model.Id);
        }

        [TestMethod]
        public async Task Delete_Post_RedirectsToIndex()
        {
            //Arrange
            var deleteModel = new DeleteStudentViewModel { Id = 1, Name = "John", LastName = "Doe" };

            //Act
            var result = await _controller.Delete(deleteModel, CancellationToken.None);
            var redirectResult = result as RedirectToActionResult;

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public async Task Create_Get_ReturnsViewResult_WithCreateStudentViewModel()
        {
            //Arrange
            var groups = new List<GroupDto> { new GroupDto { Id = 1, Name = "Group1" } };

            //Setup
            _studentsServiceMock.Setup(service => service.GetGroupsAsync(It.IsAny<CancellationToken>()))
                                .ReturnsAsync(groups);

            //Act
            var result = await _controller.Create(CancellationToken.None);
            var viewResult = result as ViewResult;
            var model = viewResult.Model as CreateStudentViewModel;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsTrue(groups.SequenceEqual(model.Groups));
        }

        [TestMethod]
        public async Task Create_Post_ValidModel_RedirectsToIndex()
        {
            //Arrange
            var createModel = new CreateStudentViewModel
            {
                Name = "John",
                LastName = "Doe",
                GroupId = 1
            };

            //Act
            var result = await _controller.Create(createModel, CancellationToken.None);
            var redirectResult = result as RedirectToActionResult;

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public async Task Create_Post_InvalidModel_ReturnsViewResult()
        {
            //Arrange
            var createModel = new CreateStudentViewModel
            {
                Name = "John",
                LastName = "Doe",
                GroupId = 1
            };
            _controller.ModelState.AddModelError("Name", "Required");

            //Act
            var result = await _controller.Create(createModel, CancellationToken.None);
            var viewResult = result as ViewResult;
            var model = viewResult.Model as CreateStudentViewModel;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(createModel.Name, model.Name);
            Assert.AreEqual(createModel.LastName, model.LastName);
            Assert.AreEqual(createModel.GroupId, model.GroupId);
        }
    }
}
