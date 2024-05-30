using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task10.Core.DTOs;
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
            ArgumentException.ThrowIfNullOrWhiteSpace(groupName, nameof(groupName));

            var group = new Group { Name = groupName, CourseId = courseId };

            await _groupsRepository.AddAsync(group);
        }

        public async Task DeleteGroupAsync(int groupId)
        {
            await _groupsRepository.DeleteAsync(groupId);
        }

        public async Task EditGroupAsync(string name, int groupId, int courseId)
        {
            Group group = await _groupsRepository.GetByIdAsync(groupId);

            group.Name = name;
            group.CourseId = courseId;

            await _groupsRepository.UpdateAsync(groupId);
        }

        public async Task<GroupEditDto> GetEditGroupDto(int id)
        {
            Group group = await _groupsRepository.GetByIdAsync(id);
            var dto = new GroupDto
            {
                Name =
                group.Name,
                CourseId = group.CourseId,
                Id = group.Id,
            };

            IEnumerable<Course> courses = await _coursesRepository.GetAllAsync();
            IEnumerable<CourseDto> courseDtos = courses.Select(c => new CourseDto { Id = c.Id, Name = c.Name });

            return new GroupEditDto { Group = dto, Courses = courseDtos };
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsWithAsync()
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
