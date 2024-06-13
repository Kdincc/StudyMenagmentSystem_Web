using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task10.Test.Core.Models;

namespace Task10.Core.Aggregates
{
    public sealed class СourseList(IEnumerable<Course> courses)
    {
        public IEnumerable<Course> Courses => courses;
    }
}
