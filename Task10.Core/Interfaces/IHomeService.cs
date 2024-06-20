using Task10.Core.Aggregates;
using Task10.Core.DTOs;

namespace Task10.Core.Interfaces
{
    public interface IHomeService
    {
        public Task<HomeDto> GetHomeDtoAsync(CancellationToken cancellationToken);

        public Task<СourseList> GetCourseListAsync(CancellationToken cancellationToken);
    }
}
