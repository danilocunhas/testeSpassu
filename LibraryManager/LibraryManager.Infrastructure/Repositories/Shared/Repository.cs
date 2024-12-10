using LibraryManager.Domain.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryManager.Infrastructure.Repositories.Shared
{
    internal abstract class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        protected readonly DbSet<TEntity> _entities;
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _entities = context.Set<TEntity>();
            _context = context;
        }

        public async ValueTask<IEnumerable<TEntity>> AllByFilterAsync(Expression<Func<TEntity, bool>> predicate, IEnumerable<string>? includes = null, bool trackingData = false, CancellationToken cancellationToken = default)
        {
            var query = _entities.Where(predicate);

            _ = includes?.Aggregate((x, y) => { query.Include(y); return y; });

            return await (trackingData ? query : query.AsNoTracking()).ToListAsync(cancellationToken);
        }

        public async ValueTask<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate, IEnumerable<string>? includes = null, bool trackingData = false, CancellationToken cancellationToken = default)
        {
            var query = _entities.Where(predicate);

            foreach (var include in includes ?? Enumerable.Empty<string>())
                query = query.Include(include);

            return await (trackingData ? query : query.AsNoTracking()).FirstOrDefaultAsync(cancellationToken);
        }

        public async ValueTask<TEntity?> FindOneByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await _entities.FindAsync(new object?[] { id }, cancellationToken);
        }

        public async ValueTask<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var result = (await _entities.AddAsync(entity, cancellationToken)).Entity;
            await _context.SaveChangesAsync(cancellationToken);
            return result;
        }

        public async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _entities.Remove(entity);
            var affectedRows = await _context.SaveChangesAsync(cancellationToken);

            return affectedRows;
        }

        public async Task<int> DeleteByFilterAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var entities = await _entities.Where(predicate).ToListAsync(cancellationToken);

            if (entities.Count != 0)
            {              
                _entities.RemoveRange(entities);             
                var affectedRows = await _context.SaveChangesAsync(cancellationToken);
                return affectedRows;
            }

            return 0; 
        }

        public async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _entities.Entry(entity).Property<DateTime?>("UpdatedAt").CurrentValue = DateTime.UtcNow;
            _entities.Update(entity);

            var affectedRows = await _context.SaveChangesAsync(cancellationToken);

            return affectedRows;
        }
    }
}
