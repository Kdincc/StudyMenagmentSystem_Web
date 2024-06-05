namespace Task10.Core.DTOs
{
    public sealed class GroupDto : IEquatable<GroupDto>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public bool Equals(GroupDto other)
        {
            return Id == other.Id;
        }
    }
}
