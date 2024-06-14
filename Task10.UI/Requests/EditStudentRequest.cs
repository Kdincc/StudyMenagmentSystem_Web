using System.ComponentModel.DataAnnotations;

namespace Task10.UI.Requests
{
    public sealed class EditStudentRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int GroupId { get; set; }
    }
}
