namespace Task10.Test.Core.Models
{
    public sealed class Student : DbEntity
    {
        public Group Group { get; set; }

        public int GroupId { get; set; }

        public string LastName { get; set; }
    }
}
