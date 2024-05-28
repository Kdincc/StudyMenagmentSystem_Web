using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task10.Core.Interfaces;
using Task10.Test.Core.Interfaces;
using Task10.Test.Core.Models;

namespace Task10.Infrastructure.Repos
{
    public sealed class GroupsRepository(CoursesDbContext dbContext) : IGroupsRepository
    {
        private readonly CoursesDbContext _coursesDbContext = dbContext;

        public async Task AddAsync(Group entity)
        {
            await _coursesDbContext.Groups.AddAsync(entity);

            await _coursesDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Group group = await _coursesDbContext.Groups.FindAsync(id);

            if (group == null)
            {
                throw new NullReferenceException(nameof(group));
            }

            _coursesDbContext.Groups.Remove(group);

            await _coursesDbContext.SaveChangesAsync();
        }

        public async Task<Group> GetByIdAsync(int id)
        {
            return await _coursesDbContext.Groups.FindAsync(id);
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _coursesDbContext.Groups.ToListAsync();
        }

        public async Task UpdateAsync(int id)
        {
            Group group = await _coursesDbContext.Groups.FindAsync(id);

            if (group == null) 
            {
                throw new NullReferenceException(nameof(group));
            }

            _coursesDbContext.Groups.Update(group);

            await _coursesDbContext.SaveChangesAsync();
        }
    }
}
