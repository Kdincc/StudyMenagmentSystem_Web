using Microsoft.EntityFrameworkCore;
using Task10.Test.Core.Interfaces;
using Task10.Test.Core.Models;

namespace Task10.Infrastructure.Repos
{
    public sealed class CourseRepository(CoursesDbContext dbContext) : ICoursesRepository
    {
        private readonly CoursesDbContext _dbContext = dbContext;

        public async Task AddAsync(Course entity, CancellationToken cancellationToken)
        {
            await _dbContext.Courses.AddAsync(entity, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            Course course = await _dbContext.Courses.FindAsync(id, cancellationToken);

            _dbContext.Courses.Remove(course);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Course>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Courses.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Course>> GetAllWithGroupsAndStudentsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Courses.Include(c => c.Groups).ThenInclude(g => g.Students).ToListAsync(cancellationToken);
        }

        public async Task<Course> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Courses.FindAsync(id, cancellationToken);
        }

        public async Task UpdateAsync(int id, CancellationToken cancellationToken)
        {
            Course course = await _dbContext.Courses.FindAsync(id, cancellationToken);

            _dbContext.Courses.Update(course);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
