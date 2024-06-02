using Task10.Core.DTOs;

namespace Task10.UI.ViewModels
{
    public sealed class EditStudentViewModel
    {
        public IEnumerable<GroupDto> Groups { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public int Id { get; set; }

        public int GroupId { get; set; }
    }
}
