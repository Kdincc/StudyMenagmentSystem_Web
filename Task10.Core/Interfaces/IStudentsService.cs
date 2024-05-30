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
        public Task<GroupEditDto> GetEditStudentDto(int id);

        public Task<IEnumerable<GroupDto>> GetStudentsWithGroupsAsync();

        public Task CreateStudentAsync(string groupName, int courseId);

        public Task DeleteStudentAsync(int groupId);

        public Task EditStudentpAsync(string name, int groupId, int courseId);

        public Task<IEnumerable<GroupDto>> GetGroupsAsync();
    }
}
