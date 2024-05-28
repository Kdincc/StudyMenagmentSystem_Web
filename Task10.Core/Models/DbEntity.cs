using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10.Test.Core.Models
{
    public abstract class DbEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
