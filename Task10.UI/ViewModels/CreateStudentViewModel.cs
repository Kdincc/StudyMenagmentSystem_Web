using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Task10.Core.DTOs;

namespace Task10.UI.ViewModels
{
    public sealed class CreateStudentViewModel
    {
        public IEnumerable<GroupDto> Groups { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Group is required, try to add some groups first")]
        [NotNull]
        public int GroupId { get; set; }
    }
}
