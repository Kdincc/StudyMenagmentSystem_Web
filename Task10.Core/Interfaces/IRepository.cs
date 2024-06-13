using Task10.Test.Core.Models;

namespace Task10.Core.Interfaces
{
    public interface IRepository<T> where T : DbEntity
    {
        public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);

        public Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);

        public Task AddAsync(T entity, CancellationToken cancellationToken);

        public Task UpdateAsync(int id, CancellationToken cancellationToken);

        public Task DeleteAsync(int id, CancellationToken cancellationToken);

        public Task<bool> ContainsAsync(int entityId, CancellationToken cancellationToken);
    }
}
