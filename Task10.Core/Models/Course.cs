using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10.Test.Core.Models
{
    public sealed class Course : DbEntity
    {
        public ICollection<Group> Groups { get; set; } = [];
    }
}
