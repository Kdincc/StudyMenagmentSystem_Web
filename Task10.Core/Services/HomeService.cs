using Task10.Core.DTOs;
using Task10.Core.Interfaces;
using Task10.Test.Core.Interfaces;
using Task10.Test.Core.Models;

namespace Task10.Core.Services
{
    public sealed class HomeService(ICoursesRepository coursesRepository, IGroupsRepository groupsRepository, IStudentsRepository studentsRepository) : IHomeService
    {
        private readonly ICoursesRepository _coursesRepository = coursesRepository;
        private readonly IGroupsRepository _groupsRepository = groupsRepository;
        private readonly IStudentsRepository _studentsRepository = studentsRepository;

        public async Task<HomeDto> GetHomeDtoAsync(CancellationToken cancellationToken)
        {
            IEnumerable<Group> groups = await _groupsRepository.GetAllAsync(cancellationToken);
            IEnumerable<Course> courses = await _coursesRepository.GetAllAsync(cancellationToken);
            IEnumerable<Student> students = await _studentsRepository.GetAllAsync(cancellationToken);

            return new HomeDto { Courses = courses, Students = students, Groups = groups };
        }
    }
}
