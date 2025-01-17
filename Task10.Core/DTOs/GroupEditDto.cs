﻿namespace Task10.Core.DTOs
{
    public sealed class GroupEditDto : IEquatable<GroupEditDto>
    {
        public GroupDto Group { get; set; }

        public IEnumerable<CourseDto> Courses { get; set; }

        public bool Equals(GroupEditDto other)
        {
            return Group.Equals(other.Group) && Courses.SequenceEqual(other.Courses);
        }
    }
}
