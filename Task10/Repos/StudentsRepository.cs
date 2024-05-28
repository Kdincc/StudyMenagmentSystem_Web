using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task DeleteAsync(Student entity)
        {
            _dbContext.Students.Remove(entity);

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

        public async Task UpdateAsync(Student entity)
        {
            _dbContext.Students.Update(entity);

            await _dbContext.SaveChangesAsync();
        }
    }
}
