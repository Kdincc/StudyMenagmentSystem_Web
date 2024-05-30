using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task10.Core.DTOs;

namespace Task10.Core.Interfaces
{
    public interface IGroupsService
    {
        public Task<GroupEditDto> GetEditGroupDto(int id);

        public Task<IEnumerable<GroupDto>> GetGroupsWithAsync();

        public Task CreateGroupAsync(string groupName, int courseId);

        public Task DeleteGroupAsync(int groupId);

        public Task EditGroupAsync(string name, int groupId, int courseId);
    }
}
