using System.Linq.Expressions;

namespace LibraryManager.Domain.Entities.Shared
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        ValueTask<TEntity?> FindOneByIdAsync(TId id, CancellationToken cancellationToken = default);
        ValueTask<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate, IEnumerable<string>? includes = null, bool trackingData = false, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<TEntity>> AllByFilterAsync(Expression<Func<TEntity, bool>> predicate, IEnumerable<string>? includes = null, bool trackingData = false, CancellationToken cancellationToken = default);
        ValueTask<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> DeleteByFilterAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
