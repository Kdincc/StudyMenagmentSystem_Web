using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10.Core.DTOs
{
    public sealed class DeleteStudentDto
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public int Id { get; set; }
    }
}
