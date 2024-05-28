using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task10.Core.DTOs;

namespace Task10.Core.Interfaces
{
    public interface IGroupService
    {
        public Task<IEnumerable<GroupDto>> GetGroupsAsync();

        public Task CreateGroupAsync(string groupName, int courseId);

        public Task DeleteGroupAsync(int groupId);

        public Task EditGroupAsync(GroupDto groupDto);
    }
}
