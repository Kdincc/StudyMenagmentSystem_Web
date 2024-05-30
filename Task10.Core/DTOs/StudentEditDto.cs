using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task10.Test.Core.Models;

namespace Task10.Core.DTOs
{
    public sealed class StudentEditDto
    {
        public StudentDto Student { get; set; }

        public IEnumerable<GroupDto> Groups { get; set; }
    }
}
