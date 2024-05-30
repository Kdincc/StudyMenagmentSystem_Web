using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10.Core.DTOs
{
    public sealed class StudentDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }    

        public int GroupId { get; set; }

        public string GroupName { get; set; }
    }
}
