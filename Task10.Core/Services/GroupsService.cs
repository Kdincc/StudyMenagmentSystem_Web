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
    public sealed class GroupsService(IGroupsRepository groupsRepository) : IGroupsService
    {
        private readonly IGroupsRepository _groupsRepository = groupsRepository;

        public async Task CreateGroupAsync(string groupName, int courseId)
        {
            ArgumentNullException.ThrowIfNull(groupName, nameof(groupName));

            var group = new Group { Name = groupName, CourseId = courseId };

            await _groupsRepository.AddAsync(group);
        }

        public async Task DeleteGroupAsync(int groupId)
        {
            await _groupsRepository.DeleteAsync(groupId);
        }

        public async Task EditGroupAsync(GroupDto groupDto)
        {
            await _groupsRepository.UpdateAsync(groupDto.Id);
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsAsync()
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
