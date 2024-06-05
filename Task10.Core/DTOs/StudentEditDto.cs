namespace Task10.Core.DTOs
{
    public sealed class StudentEditDto : IEquatable<StudentEditDto>
    {
        public StudentDto Student { get; set; }

        public IEnumerable<GroupDto> Groups { get; set; }

        public bool Equals(StudentEditDto other)
        {
            return Student.Equals(other.Student) && Groups.SequenceEqual(other.Groups);
        }
    }
}
