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
    public sealed class HomeService(ICoursesRepository coursesRepository, IGroupsRepository groupsRepository, IStudentsRepository studentsRepository) : IHomeService
    {
        private readonly ICoursesRepository _coursesRepository = coursesRepository;
        private readonly IGroupsRepository _groupsRepository = groupsRepository;
        private readonly IStudentsRepository _studentsRepository = studentsRepository;

        public async Task<HomeDto> GetHomeDtoAsync()
        {
            IEnumerable<Group> groups = await _groupsRepository.GetAllAsync();
            IEnumerable<Course> courses = await _coursesRepository.GetAllAsync();
            IEnumerable<Student> students = await _studentsRepository.GetAllAsync();

            return new HomeDto { Courses =  courses, Students = students, Groups = groups };
        }
    }
}
