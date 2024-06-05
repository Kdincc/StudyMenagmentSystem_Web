using Microsoft.EntityFrameworkCore;
using Task10.Test.Core.Interfaces;
using Task10.Test.Core.Models;

namespace Task10.Infrastructure.Repos
{
    public sealed class StudentsRepository(CoursesDbContext dbContext) : IStudentsRepository
    {
        private readonly CoursesDbContext _dbContext = dbContext;

        public async Task AddAsync(Student entity)
        {
            await _dbContext.Students.AddAsync(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Student student = await _dbContext.Students.FindAsync(id);

            _dbContext.Students.Remove(student);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _dbContext.Students.FindAsync(id);
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task UpdateAsync(int id)
        {
            Student student = await _dbContext.Students.FindAsync(id);

            _dbContext.Students.Update(student);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentWithGroupsAsync()
        {
            return await _dbContext.Students.Include(s => s.Group).ToListAsync();
        }
    }
}
