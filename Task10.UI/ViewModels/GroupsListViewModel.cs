using Task10.Core.DTOs;

namespace Task10.UI.ViewModels
{
    public sealed class GroupsListViewModel
    {
        public IEnumerable<GroupDto> Groups { get; set; }
    }
}
