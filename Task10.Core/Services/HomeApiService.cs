using Task10.Core.Aggregates;
using Task10.Core.Interfaces;
using Task10.Test.Core.Interfaces;
using Task10.Test.Core.Models;

namespace Task10.Core.Services
{
    public sealed class HomeApiService(ICoursesRepository coursesRepository) : IHomeApiService
    {
        private readonly ICoursesRepository _coursesRepository = coursesRepository;

        public async Task<СourseList> GetCourses(CancellationToken cancellationToken)
        {
            IEnumerable<Course> courses = await _coursesRepository.GetAllAsync(cancellationToken);

            return new СourseList(courses);
        }
    }
}
