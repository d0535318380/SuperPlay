using System.Linq.Expressions;

namespace SuperPlay.Abstractions.Data;

public interface IGenericRepository<TKey, TEntity> where TEntity : class
{
    ValueTask<TEntity?> FindAsync(TKey id, CancellationToken token = default);
    Task<IReadOnlyCollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default);
    Task<int> AddAsync(TEntity entity, CancellationToken token = default);
    Task<int> UpdateAsync(TEntity entity, CancellationToken token = default);
}