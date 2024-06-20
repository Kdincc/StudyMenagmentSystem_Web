namespace Task10.Test.Core.Models
{
    public sealed class Course : DbEntity
    {
        public ICollection<Group> Groups { get; set; } = [];
    }
}
