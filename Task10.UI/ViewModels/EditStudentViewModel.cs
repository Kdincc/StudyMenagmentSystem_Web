using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Task10.Core.DTOs;

namespace Task10.UI.ViewModels
{
    public sealed class EditStudentViewModel
    {
        [JsonIgnore]
        public IEnumerable<GroupDto> Groups { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(30)]
        public string LastName { get; set; }

        public int Id { get; set; }

        [Required]
        public int GroupId { get; set; }
    }
}
