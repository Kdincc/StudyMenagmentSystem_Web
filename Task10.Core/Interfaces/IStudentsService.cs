using Task10.Core.DTOs;

namespace Task10.Core.Interfaces
{
    public interface IStudentsService
    {
        public Task<StudentEditDto> GetEditStudentDtoAsync(int id, CancellationToken cancellationToken);

        public Task<IEnumerable<StudentDto>> GetStudentsWithGroupsNameAsync(CancellationToken cancellationToken);

        public Task CreateStudentAsync(string studentName, string lastName, int groupId, CancellationToken cancellationToken);

        public Task DeleteStudentAsync(int studentId, CancellationToken cancellationToken);

        public Task EditStudentAsync(string name, string lastName, int studentId, int groupId, CancellationToken cancellationToken);

        public Task<IEnumerable<GroupDto>> GetGroupsAsync(CancellationToken cancellationToken);

        public Task<DeleteStudentDto> GetDeleteStudentDto(int id, CancellationToken cancellationToken);

        public Task<bool> IsStudentExistsAsync(int studentId, CancellationToken cancellationToken);

        public Task<bool> IsGroupExistsAsync(int groupId, CancellationToken cancellationToken);
    }
}
