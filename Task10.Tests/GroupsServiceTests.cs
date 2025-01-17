﻿using Moq;
using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.Core.Services;
using Task10.Test.Core.Interfaces;
using Task10.Test.Core.Models;

namespace Task10.Tests
{
    [TestClass]
    public class GroupsServiceTests
    {
        private readonly IGroupsService _groupsService;
        private readonly Mock<ICoursesRepository> _coursesRepositoryMoq;
        private readonly Mock<IGroupsRepository> _groupsRepositoryMoq;

        public GroupsServiceTests()
        {
            _coursesRepositoryMoq = new();
            _groupsRepositoryMoq = new();

            _groupsService = new GroupsService(_groupsRepositoryMoq.Object, _coursesRepositoryMoq.Object);
        }

        [TestMethod]
        public async Task GetCoursesAsync_IsCorrectDataReturns()
        {
            //Arrange
            IEnumerable<Course> courses = [new Course() { Name = "TestName", Id = 1 }, new Course() { Name = "TestName1", Id = 2 }];
            IEnumerable<CourseDto> expected = [new CourseDto() { Name = "TestName", Id = 1 }, new CourseDto() { Name = "TestName1", Id = 2 }];

            //Setup
            _coursesRepositoryMoq.Setup(m => m.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(courses);

            //Act
            IEnumerable<CourseDto> actual = await _groupsService.GetCoursesAsync(It.IsAny<CancellationToken>());

            //Assert
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public async Task GetEditGroupDto_IsCorrectDataReturns()
        {
            //Arrange
            int id = 1;
            IEnumerable<Course> courses = [new Course() { Name = "TestName", Id = 1 }, new Course() { Name = "TestName1", Id = 2 }];
            IEnumerable<CourseDto> dtos = [new CourseDto() { Name = "TestName", Id = 1 }, new CourseDto() { Name = "TestName1", Id = 2 }];
            Group group = new() { Name = "TestName", Id = 1, CourseId = 1 };
            GroupDto groupDto = new() { Name = group.Name, Id = group.Id, CourseId = group.CourseId };
            GroupEditDto expected = new() { Courses = dtos, Group = groupDto };

            //Setup
            _coursesRepositoryMoq.Setup(m => m.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(courses);
            _groupsRepositoryMoq.Setup(m => m.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(group);

            //Act
            GroupEditDto actual = await _groupsService.GetEditGroupDtoAsync(id, It.IsAny<CancellationToken>());

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task GetGroupsWithCourseNamesAsync_IsCorrectDataReturns()
        {
            //Arrange
            IEnumerable<Group> groups =
                [
                    new Group { Name = "TestName", Id = 1, CourseId = 1, Course = new Course() { Name = "TestName", Id = 1 } },
                    new Group() { Name = "TestName1", Id = 2, CourseId = 2, Course = new Course() { Name = "TestName1", Id = 2 } }
                ];
            IEnumerable<GroupDto> expected = groups.Select(g => new GroupDto { Id = g.Id, CourseId = g.CourseId, Name = g.Name, CourseName = g.Course.Name });

            //Setup
            _groupsRepositoryMoq.Setup(m => m.GetGroupsWithCoursesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(groups);

            //Act
            IEnumerable<GroupDto> actual = await _groupsService.GetGroupsWithCourseNamesAsync(It.IsAny<CancellationToken>());

            //Assert
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public async Task EditGroup_IsEditedInRepository()
        {
            //Arrange
            int id = 1;
            string name = "new name";
            int courseId = 2;
            Group groupToEdit = new() { Name = "old name", CourseId = 1, Id = 1 };

            //Setup
            _groupsRepositoryMoq.Setup(m => m.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(groupToEdit);

            //Act
            await _groupsService.EditGroupAsync(name, id, courseId, It.IsAny<CancellationToken>());

            //Assert
            _groupsRepositoryMoq.Verify(m => m.UpdateAsync(id, It.IsAny<CancellationToken>()), Times.Once);
        }


        [TestMethod]
        public async Task DeleteGroup_GroupWithoutStudents_IsDeletedInRepository()
        {
            //Arrange
            int id = 1;
            Group groupToDelete = new() { Name = "old name", CourseId = 1, Id = 1 };

            //Setup
            _groupsRepositoryMoq.Setup(m => m.GetGroupWithStudents(id, It.IsAny<CancellationToken>())).ReturnsAsync(groupToDelete);

            //Act
            await _groupsService.DeleteGroupAsync(id, It.IsAny<CancellationToken>());

            //Assert
            _groupsRepositoryMoq.Verify(m => m.DeleteAsync(id, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteGroup_GroupWithStudents_NotDeletedInRepository()
        {
            //Arrange
            int id = 1;
            Group groupToDelete = new() { Name = "old name", CourseId = 1, Id = 1, Students = [new Student()] };

            //Setup
            _groupsRepositoryMoq.Setup(m => m.GetGroupWithStudents(id, It.IsAny<CancellationToken>())).ReturnsAsync(groupToDelete);

            //Act
            await _groupsService.DeleteGroupAsync(id, It.IsAny<CancellationToken>());

            //Assert
            _groupsRepositoryMoq.Verify(m => m.DeleteAsync(id, It.IsAny<CancellationToken>()), Times.Never);
        }

        [TestMethod]
        public async Task CreateGroup_IsCreatedInRepository()
        {
            //Arrange
            string name = "name";
            int courseId = 1;

            //Act
            await _groupsService.CreateGroupAsync(name, courseId, It.IsAny<CancellationToken>());

            //Assert
            _groupsRepositoryMoq.Verify(m => m.AddAsync(It.IsAny<Group>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
