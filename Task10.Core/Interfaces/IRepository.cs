using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task10.Test.Core.Models;

namespace Task10.Core.Interfaces
{
    public interface IRepository<T> where T : DbEntity
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T> GetByIdAsync(int id);

        public Task AddAsync(T entity);

        public Task UpdateAsync(int id);

        public Task DeleteAsync(int id);
    }
}
