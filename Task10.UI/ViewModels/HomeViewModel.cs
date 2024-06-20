using Task10.Test.Core.Models;

namespace Task10.UI.ViewModels
{
    public sealed class HomeViewModel
    {
        public IEnumerable<Course> Courses { get; set; }

        public IEnumerable<Group> Groups { get; set; }

        public IEnumerable<Student> Students { get; set; }
    }
}
