namespace SuperPlay.Abstractions.Data;

public interface IGenericRepository<TKey, TEntity> where TEntity : class
{
    Task<TEntity> FindAsync(TKey id, CancellationToken token = default);
    Task AddAsync(TEntity entity, CancellationToken token = default);
    Task UpdateAsync(TEntity entity, CancellationToken token = default);
}