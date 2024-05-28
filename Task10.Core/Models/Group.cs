using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10.Test.Core.Models
{
    public sealed class Group : DbEntity
    {
        public ICollection<Student> Students { get; set; } = [];

        public Course Course { get; set; }

        public int CourseId { get; set; }
    }
}
