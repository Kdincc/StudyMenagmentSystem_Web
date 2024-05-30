using Task10.Core.DTOs;

namespace Task10.UI.ViewModels
{
    public sealed class CreateGroupViewModel
    {
        public IEnumerable<CourseDto> Courses { get; set; }
    }
}
