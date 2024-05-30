using Task10.Core.DTOs;

namespace Task10.UI.ViewModels
{
    public sealed class EditGroupViewModel
    {
        public IEnumerable<CourseDto> Courses { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }

        public int CourseId { get; set; }
    }
}
