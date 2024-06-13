using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task10.Core.Aggregates;
using Task10.Test.Core.Models;

namespace Task10.Core.Interfaces
{
    public interface IHomeApiService
    {
        public Task<СourseList> GetCourses(CancellationToken cancellationToken);
    }
}
