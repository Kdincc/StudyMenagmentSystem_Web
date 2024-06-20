namespace Task10.Core.DTOs
{
    public sealed class CourseDto : IEquatable<CourseDto>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Equals(CourseDto other)
        {
            return Id == other.Id;
        }
    }
}
