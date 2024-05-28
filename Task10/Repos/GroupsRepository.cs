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

        public async Task DeleteAsync(Group entity)
        {
            _coursesDbContext.Groups.Remove(entity);

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

        public async Task UpdateAsync(Group entity)
        {
            _coursesDbContext.Groups.Update(entity);

            await _coursesDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Group>> GetGroupsWithCoursesAsync()
        {
            return await _coursesDbContext.Groups.Include(g => g.Course).ToListAsync();
        }
    }
}
