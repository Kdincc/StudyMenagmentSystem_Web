using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10.Core.DTOs
{
    public class GroupEditDto
    {
        public GroupDto Group { get; set; }

        public IEnumerable<CourseDto> Courses { get; set; }
    }
}
