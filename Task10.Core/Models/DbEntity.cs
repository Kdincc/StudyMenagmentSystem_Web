using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10.Test.Core.Models
{
    public abstract class DbEntity : IEquatable<DbEntity>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Equals(DbEntity other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DbEntity);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Id);
        }
    }
}
