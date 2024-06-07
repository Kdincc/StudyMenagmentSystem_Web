using Microsoft.EntityFrameworkCore;
using Task10.Test.Core.Interfaces;
using Task10.Test.Core.Models;

namespace Task10.Infrastructure.Repos
{
    public sealed class StudentsRepository(CoursesDbContext dbContext) : IStudentsRepository
    {
        private readonly CoursesDbContext _dbContext = dbContext;

        public async Task AddAsync(Student entity, CancellationToken cancellationToken)
        {
            await _dbContext.Students.AddAsync(entity, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            Student student = await _dbContext.Students.FindAsync(id, cancellationToken);

            _dbContext.Students.Remove(student);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Student> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Students.FindAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Students.ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(int id, CancellationToken cancellationToken)
        {
            Student student = await _dbContext.Students.FindAsync(id, cancellationToken);

            _dbContext.Students.Update(student);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Student>> GetStudentWithGroupsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Students.Include(s => s.Group).ToListAsync(cancellationToken);
        }
    }
}
