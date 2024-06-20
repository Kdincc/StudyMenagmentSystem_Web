using Microsoft.EntityFrameworkCore;
using Task10.Test.Core.Interfaces;
using Task10.Test.Core.Models;

namespace Task10.Infrastructure.Repos
{
    public sealed class GroupsRepository(CoursesDbContext dbContext) : IGroupsRepository
    {
        private readonly CoursesDbContext _coursesDbContext = dbContext;

        public async Task AddAsync(Group entity, CancellationToken cancellationToken)
        {
            await _coursesDbContext.Groups.AddAsync(entity, cancellationToken);

            await _coursesDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            Group group = await _coursesDbContext.Groups.FindAsync(id, cancellationToken);

            _coursesDbContext.Groups.Remove(group);

            await _coursesDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Group> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _coursesDbContext.Groups.FindAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Group>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _coursesDbContext.Groups.ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(int id, CancellationToken cancellationToken)
        {
            Group group = await _coursesDbContext.Groups.FindAsync(id, cancellationToken);

            _coursesDbContext.Groups.Update(group);

            await _coursesDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Group>> GetGroupsWithCoursesAsync(CancellationToken cancellationToken)
        {
            return await _coursesDbContext.Groups.Include(g => g.Course).ToListAsync(cancellationToken);
        }

        public async Task<Group> GetGroupWithStudents(int id, CancellationToken cancellationToken)
        {
            Group group = await _coursesDbContext.Groups.Include(g => g.Students).FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

            return group;
        }

        public async Task<bool> ContainsAsync(int entityId, CancellationToken cancellationToken)
        {
            Group group = await _coursesDbContext.Groups.FindAsync(entityId, cancellationToken);

            if (group is null)
            {
                return false;
            }

            return true;
        }
    }
}
