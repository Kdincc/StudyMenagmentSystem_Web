using Task10.Core.Interfaces;
using Task10.Test.Core.Models;

namespace Task10.Test.Core.Interfaces
{
    public interface ICoursesRepository : IRepository<Course>
    {
        public Task<IEnumerable<Course>> GetAllWithGroupsAndStudentsAsync(CancellationToken cancellationToken);
    }
}
