using Task10.Core.DTOs;

namespace Task10.Core.Interfaces
{
    public interface IGroupsService
    {
        public Task<GroupEditDto> GetEditGroupDtoAsync(int id, CancellationToken cancellationToken);

        public Task<IEnumerable<GroupDto>> GetGroupsWithCourseNamesAsync(CancellationToken cancellationToken);

        public Task CreateGroupAsync(string groupName, int courseId, CancellationToken cancellationToken);

        public Task<bool> DeleteGroupAsync(int groupId, CancellationToken cancellationToken);

        public Task EditGroupAsync(string name, int groupId, int courseId, CancellationToken cancellationToken);

        public Task<IEnumerable<CourseDto>> GetCoursesAsync(CancellationToken cancellationToken);

        public Task<DeleteGroupDto> GetDeleteGroupDto(int id, CancellationToken cancellationToken);
    }
}
