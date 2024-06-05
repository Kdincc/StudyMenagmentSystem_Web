using Task10.Test.Core.Models;

namespace Task10.Core.DTOs
{
    public sealed class HomeDto : IEquatable<HomeDto>
    {
        public IEnumerable<Course> Courses { get; set; }

        public IEnumerable<Group> Groups { get; set; }

        public IEnumerable<Student> Students { get; set; }

        public bool Equals(HomeDto other)
        {
            return Courses.SequenceEqual(other.Courses) && Groups.SequenceEqual(other.Groups) && Students.SequenceEqual(Students);
        }
    }
}
