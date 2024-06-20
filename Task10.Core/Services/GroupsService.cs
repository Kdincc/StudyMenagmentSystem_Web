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

        public async Task CreateGroupAsync(string groupName, int courseId, CancellationToken cancellationToken)
        {
            var group = new Group { Name = groupName, CourseId = courseId };

            await _groupsRepository.AddAsync(group, cancellationToken);
        }

        public async Task<bool> DeleteGroupAsync(int groupId, CancellationToken cancellationToken)
        {
            Group group = await _groupsRepository.GetGroupWithStudents(groupId, cancellationToken);

            if (group.Students.Count != 0)
            {
                return false;
            }

            await _groupsRepository.DeleteAsync(groupId, cancellationToken);

            return true;
        }

        public async Task EditGroupAsync(string name, int groupId, int courseId, CancellationToken cancellationToken)
        {
            Group group = await _groupsRepository.GetByIdAsync(groupId, cancellationToken);

            group.Name = name;
            group.CourseId = courseId;

            await _groupsRepository.UpdateAsync(groupId, cancellationToken);
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesAsync(CancellationToken cancellationToken)
        {
            IEnumerable<Course> courses = await _coursesRepository.GetAllAsync(cancellationToken);
            IEnumerable<CourseDto> courseDtos = courses.Select(c => new CourseDto { Id = c.Id, Name = c.Name });

            return courseDtos;
        }

        public async Task<DeleteGroupDto> GetDeleteGroupDto(int id, CancellationToken cancellationToken)
        {
            Group group = await _groupsRepository.GetByIdAsync(id, cancellationToken);

            return new DeleteGroupDto() { Id = id, Name = group.Name };
        }

        public async Task<GroupEditDto> GetEditGroupDtoAsync(int id, CancellationToken cancellationToken)
        {
            Group group = await _groupsRepository.GetByIdAsync(id, cancellationToken);
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
            IEnumerable<Group> groups = await _groupsRepository.GetGroupsWithCoursesAsync(cancellationToken);

            return groups.Select(group => new GroupDto
            {
                Id = group.Id,
                CourseId = group.CourseId,
                Name = group.Name,
                CourseName = group.Course.Name
            });
        }

        public async Task<bool> IsCourseExistsAsync(int courseId, CancellationToken cancellationToken)
        {
            if (await _coursesRepository.ContainsAsync(courseId, cancellationToken))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsGroupExistsAsync(int groupId, CancellationToken cancellationToken)
        {
            if (await _groupsRepository.ContainsAsync(groupId, cancellationToken))
            {
                return true;
            }

            return false;
        }
    }
}
