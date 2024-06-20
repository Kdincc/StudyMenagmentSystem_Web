using Moq;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.Core.Services;
using Task10.Test.Core.Interfaces;
using Task10.Test.Core.Models;

namespace Task10.Tests
{
    [TestClass]
    public class StudentServiceTests
    {
        private readonly Mock<IStudentsRepository> _studentRepositoryMock;
        private readonly Mock<IGroupsRepository> _groupRepositoryMock;
        private readonly IStudentsService _studentsService;

        public StudentServiceTests()
        {
            _studentRepositoryMock = new Mock<IStudentsRepository>();
            _groupRepositoryMock = new Mock<IGroupsRepository>();

            _studentsService = new StudentsService(_studentRepositoryMock.Object, _groupRepositoryMock.Object);
        }

        [TestMethod]
        public async Task CreateStudent_IsAddedInRepository()
        {
            //Arrange
            int groupId = 1;
            string name = "name";
            string lastName = "lastName";

            //Act
            await _studentsService.CreateStudentAsync(name, lastName, groupId, It.IsAny<CancellationToken>());

            //Assert
            _studentRepositoryMock.Verify(m => m.AddAsync(It.IsAny<Student>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteStudent_IsDeletedInRepository()
        {
            //Arrange
            int studentId = 1;

            //Act
            await _studentsService.DeleteStudentAsync(studentId, It.IsAny<CancellationToken>());

            //Assert
            _studentRepositoryMock.Verify(m => m.DeleteAsync(studentId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task EditStudent_IsUpdatedInRepository()
        {
            //Arrange
            int studentId = 1;
            int grouoId = 1;
            string name = "newName";
            string lastName = "newLastName";

            //Setup
            _studentRepositoryMock.Setup(m => m.GetByIdAsync(studentId, It.IsAny<CancellationToken>())).ReturnsAsync(new Student());

            //Act
            await _studentsService.EditStudentAsync(name, lastName, studentId, grouoId, It.IsAny<CancellationToken>());

            //Assert
            _studentRepositoryMock.Verify(m => m.UpdateAsync(studentId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task GetEditStudentDtoAsync_IsCorrectDataReturns()
        {
            //Arrange
            int id = 1;
            Student student = new() { Name = "name", LastName = "lastName", Id = 1, GroupId = 1, };
            IEnumerable<Group> groups =
                [
                    new Group { Name = "TestName", Id = 1, CourseId = 1, Course = new Course() { Name = "TestName", Id = 1 } },
                    new Group() { Name = "TestName1", Id = 2, CourseId = 2, Course = new Course() { Name = "TestName1", Id = 2 } }
                ];
            IEnumerable<GroupDto> groupDtos = groups.Select(g => new GroupDto { Id = g.Id, CourseId = g.CourseId, Name = g.Name, CourseName = g.Course.Name });
            StudentDto studentDto = new() { Name = "name", LastName = "lastName", GroupName = "name", GroupId = 1, Id = 1 };
            StudentEditDto expected = new() { Groups = groupDtos, Student = studentDto };

            //Setup
            _studentRepositoryMock.Setup(m => m.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(student);
            _groupRepositoryMock.Setup(m => m.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(groups);

            //Act
            StudentEditDto actual = await _studentsService.GetEditStudentDtoAsync(id, It.IsAny<CancellationToken>());

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task GetStudentsWithGroupsNameAsync_IsCorrectDataReturns()
        {
            //Arrange
            IEnumerable<Student> students =
                [
                    new Student() { Name = "name", LastName = "lastName", GroupId = 1, Group = new Group() { Name = "name", Id = 1 } },
                    new Student() { Name = "name1", LastName = "lastName1", GroupId = 2, Group = new Group() { Name = "name", Id = 2 } }
                ];
            IEnumerable<StudentDto> expected = students.Select(s => new StudentDto() { Name = s.Name, LastName = s.LastName, Id = s.Id, GroupId = s.Group.Id, GroupName = s.Group.Name });

            //Setup
            _studentRepositoryMock.Setup(m => m.GetStudentWithGroupsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(students);

            //Act
            IEnumerable<StudentDto> actual = await _studentsService.GetStudentsWithGroupsNameAsync(It.IsAny<CancellationToken>());

            //Assert
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
}
