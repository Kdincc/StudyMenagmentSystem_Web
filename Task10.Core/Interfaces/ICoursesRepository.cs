using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task10.Core.Interfaces;
using Task10.Test.Core.Models;

namespace Task10.Test.Core.Interfaces
{
    public interface ICoursesRepository : IRepository<Course>
    {
    }
}
