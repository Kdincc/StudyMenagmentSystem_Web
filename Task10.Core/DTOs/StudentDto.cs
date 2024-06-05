namespace Task10.Core.DTOs
{
    public sealed class StudentDto : IEquatable<StudentDto>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public bool Equals(StudentDto other)
        {
            return Id == other.Id;
        }
    }
}
