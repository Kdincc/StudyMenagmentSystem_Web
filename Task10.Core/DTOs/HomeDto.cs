using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task10.Test.Core.Models;

namespace Task10.Core.DTOs
{
    public sealed class HomeDto
    {
        public IEnumerable<Course> Courses { get; set; }

        public IEnumerable<Group> Groups { get; set; }

        public IEnumerable<Student> Students { get; set; }
    }
}
