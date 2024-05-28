using Microsoft.EntityFrameworkCore;
using Task10.Test.Core.Interfaces;
using Task10.Test.Core.Models;

namespace Task10.Infrastructure.Repos
{
    public sealed class CourseRepository(CoursesDbContext dbContext) : ICoursesRepository
    {
        private readonly CoursesDbContext _dbContext = dbContext;

        public async Task AddAsync(Course entity)
        {
            await _dbContext.Courses.AddAsync(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Course course = await _dbContext.Courses.FindAsync(id);

            if (course is null)
            {
                throw new NullReferenceException(nameof(course));
            }

            _dbContext.Courses.Remove(course);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _dbContext.Courses.ToListAsync();
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await _dbContext.Courses.FindAsync(id);
        }

        public async Task UpdateAsync(int id)
        {
            Course course = await _dbContext.Courses.FindAsync(id);

            if (course is null) 
            {
                throw new NullReferenceException(nameof(course));
            }

            _dbContext.Courses.Update(course);

            await _dbContext.SaveChangesAsync();
        }
    }
}
