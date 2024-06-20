using System.Text.Json.Serialization;

namespace Task10.Test.Core.Models
{
    public sealed class Group : DbEntity
    {
        public ICollection<Student> Students { get; set; } = [];

        [JsonIgnore]
        public Course Course { get; set; }

        public int CourseId { get; set; }
    }
}
