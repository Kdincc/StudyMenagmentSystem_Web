using Task10.Core.DTOs;

namespace Task10.UI.ViewModels
{
    public sealed class StudentListViewModel
    {
        public IEnumerable<StudentDto> Students { get; set; }
    }
}
