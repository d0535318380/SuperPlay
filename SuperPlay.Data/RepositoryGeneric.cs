using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SuperPlay.Abstractions.Data;
using SuperPlay.Abstractions.Extensions;

namespace SuperPlay.Data;

public class RepositoryGeneric<TKey, TEntity> : IGenericRepository<TKey, TEntity> where TEntity : class
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public RepositoryGeneric(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }
    
    public virtual ValueTask<TEntity?> FindAsync(TKey id, CancellationToken token = default)
    {
        return _dbSet.FindAsync(id, token);
    }

    public virtual async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default)
    {
        predicate.ThrowIfNull(nameof(predicate));
        
        var items = await _dbSet.Where(predicate).FirstOrDefaultAsync(token);

        return items;
    }

    public virtual async Task<int> AddAsync(TEntity entity, CancellationToken token = default)
    {
        entity.ThrowIfNull(nameof(entity));
        
        _dbSet.Add(entity);
        
        await _dbSet.AddAsync(entity, token);
        
        var count =  await _context.SaveChangesAsync(token);

        return count;
    }

    public virtual async Task<int> UpdateAsync(TEntity entity, CancellationToken token = default)
    {
        entity.ThrowIfNull(nameof(entity));
        
        _dbSet.Update(entity);
        
        var count =  await _context.SaveChangesAsync(token);

        return count;
    }
}