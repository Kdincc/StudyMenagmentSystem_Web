﻿using Task10.Core.Interfaces;
using Task10.Test.Core.Models;

namespace Task10.Test.Core.Interfaces
{
    public interface IGroupsRepository : IRepository<Group>
    {
        public Task<IEnumerable<Group>> GetGroupsWithCoursesAsync(CancellationToken cancellationToken);

        public Task<Group> GetGroupWithStudents(int id, CancellationToken cancellationToken);
    }
}
