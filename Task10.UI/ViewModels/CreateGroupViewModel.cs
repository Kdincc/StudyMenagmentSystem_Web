using System.ComponentModel.DataAnnotations;
using Task10.Core.DTOs;

namespace Task10.UI.ViewModels
{
    public sealed class CreateGroupViewModel
    {
        public IEnumerable<CourseDto> Courses { get; set; }

        [Required(ErrorMessage = "Group name is required")]
        [StringLength(30, ErrorMessage = "Group name cannot be longer than 30")]
        public string Name { get; set; }

        [Required]
        public int CourseId { get; set; }
    }
}
