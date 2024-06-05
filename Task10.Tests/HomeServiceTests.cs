using Moq;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.Core.Services;
using Task10.Test.Core.Interfaces;
using Task10.Test.Core.Models;

namespace Task10.Tests
{
    [TestClass]
    public class HomeServiceTests
    {
        private readonly Mock<IStudentsRepository> _studentsRepositoryMoq;
        private readonly Mock<IGroupsRepository> _groupsRepositoryMoq;
        private readonly Mock<ICoursesRepository> _coursesRepositoryMoq;
        private readonly IHomeService _homeService;

        public HomeServiceTests()
        {
            _coursesRepositoryMoq = new();
            _studentsRepositoryMoq = new();
            _groupsRepositoryMoq = new();

            _homeService = new HomeService(_coursesRepositoryMoq.Object, _groupsRepositoryMoq.Object, _studentsRepositoryMoq.Object);
        }

        [TestMethod]
        public async Task GetHomeDto_IsCorrectDataReturns()
        {
            //Arrange
            IEnumerable<Course> courses = [new Course() { Name = "TestName", Id = 1 }, new Course() { Name = "TestName1", Id = 2 }];
            IEnumerable<Group> groups = [new Group { Name = "TestName", Id = 1 }, new Group() { Name = "TestName1", Id = 2 }];
            IEnumerable<Student> students = [new Student() { Name = "TestName", Id = 1 }, new Student() { Name = "TestName1", Id = 2 }];
            HomeDto expected = new() { Courses = courses, Groups = groups, Students = students };

            //Setup
            _coursesRepositoryMoq.Setup(m => m.GetAllAsync()).ReturnsAsync(courses);
            _groupsRepositoryMoq.Setup(m => m.GetAllAsync()).ReturnsAsync(groups);
            _studentsRepositoryMoq.Setup(m => m.GetAllAsync()).ReturnsAsync(students);

            //Act
            HomeDto actual = await _homeService.GetHomeDtoAsync();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}