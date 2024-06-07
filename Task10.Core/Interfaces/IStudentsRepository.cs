using Task10.Core.Interfaces;
using Task10.Test.Core.Models;

namespace Task10.Test.Core.Interfaces
{
    public interface IStudentsRepository : IRepository<Student>
    {
        public Task<IEnumerable<Student>> GetStudentWithGroupsAsync(CancellationToken cancellationToken);
    }
}
