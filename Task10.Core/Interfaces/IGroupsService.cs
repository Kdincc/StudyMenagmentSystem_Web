using Task10.Core.DTOs;

namespace Task10.Core.Interfaces
{
    public interface IGroupsService
    {
        public Task<GroupEditDto> GetEditGroupDto(int id, CancellationToken cancellationToken);

        public Task<IEnumerable<GroupDto>> GetGroupsWithCourseNamesAsync(CancellationToken cancellationToken);

        public Task CreateGroupAsync(string groupName, int courseId);

        public Task<bool> DeleteGroupAsync(int groupId);

        public Task EditGroupAsync(string name, int groupId, int courseId);

        public Task<IEnumerable<CourseDto>> GetCoursesAsync(CancellationToken cancellationToken);
    }
}
