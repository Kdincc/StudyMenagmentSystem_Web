using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Task10.Core.DTOs;

namespace Task10.UI.ViewModels
{
    public sealed class EditGroupViewModel
    {
        [JsonIgnore]
        public IEnumerable<CourseDto> Courses { get; set; }

        [Required(ErrorMessage = "Group name is required")]
        [StringLength(30, ErrorMessage = "Group name cannot be longer than 30")]
        public string Name { get; set; }

        [Required]
        public int Id { get; set; }

        [Required]
        public int CourseId { get; set; }
    }
}
