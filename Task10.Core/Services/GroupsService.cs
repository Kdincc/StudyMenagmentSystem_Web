﻿using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.Test.Core.Interfaces;
using Task10.Test.Core.Models;

namespace Task10.Core.Services
{
    public sealed class GroupsService(IGroupsRepository groupsRepository, ICoursesRepository coursesRepository) : IGroupsService
    {
        private readonly IGroupsRepository _groupsRepository = groupsRepository;
        private readonly ICoursesRepository _coursesRepository = coursesRepository;

        public async Task CreateGroupAsync(string groupName, int courseId)
        {
            var group = new Group { Name = groupName, CourseId = courseId };

            await _groupsRepository.AddAsync(group);
        }

        public async Task<bool> DeleteGroupAsync(int groupId)
        {
            Group group = await _groupsRepository.GetGroupWithStudents(groupId);

            if (group.Students.Count != 0)
            {
                return false;
            }

            await _groupsRepository.DeleteAsync(groupId);

            return true;
        }

        public async Task EditGroupAsync(string name, int groupId, int courseId)
        {
            Group group = await _groupsRepository.GetByIdAsync(groupId);

            group.Name = name;
            group.CourseId = courseId;

            await _groupsRepository.UpdateAsync(groupId);
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesAsync(CancellationToken cancellationToken)
        {
            IEnumerable<Course> courses = await _coursesRepository.GetAllAsync();
            IEnumerable<CourseDto> courseDtos = courses.Select(c => new CourseDto { Id = c.Id, Name = c.Name });

            return courseDtos;
        }

        public async Task<GroupEditDto> GetEditGroupDto(int id, CancellationToken cancellationToken)
        {
            Group group = await _groupsRepository.GetByIdAsync(id);
            var dto = new GroupDto
            {
                Name =
                group.Name,
                CourseId = group.CourseId,
                Id = group.Id,
            };

            IEnumerable<CourseDto> courseDtos = await GetCoursesAsync(cancellationToken);

            return new GroupEditDto { Group = dto, Courses = courseDtos };
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsWithCourseNamesAsync(CancellationToken cancellationToken)
        {
            IEnumerable<Group> groups = await _groupsRepository.GetGroupsWithCoursesAsync();

            return groups.Select(group => new GroupDto
            {
                Id = group.Id,
                CourseId = group.CourseId,
                Name = group.Name,
                CourseName = group.Course.Name
            });
        }
    }
}
