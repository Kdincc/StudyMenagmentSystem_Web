using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task10.Core.DTOs;

namespace Task10.Core.Interfaces
{
    public interface IStudentsService
    {
        public Task<StudentEditDto> GetEditStudentDtoAsync(int id);

        public Task<IEnumerable<StudentDto>> GetStudentsWithGroupsNameAsync();

        public Task CreateStudentAsync(string studentName, string lastName, int groupId);

        public Task DeleteStudentAsync(int studentId);

        public Task EditStudentAsync(string name, string lastName, int studentId, int groupId);

        public Task<IEnumerable<GroupDto>> GetGroupsAsync();
    }
}
