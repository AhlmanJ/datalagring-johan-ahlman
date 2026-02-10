using EducationPlatform.Domain.Interfaces;
using EducationPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EducationPlatform.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly EducationPlatformDbContext _context;
    protected readonly DbSet<TEntity> _table;

    public BaseRepository(EducationPlatformDbContext context)
    {  
        _context = context;
        _table = _context.Set<TEntity>();
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        _table.Add(entity);
        return entity;
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _table.Remove(entity);
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> findBy, CancellationToken cancellationToken)
    {
        if (findBy == null) throw new ArgumentNullException(nameof(findBy));

        return await _table.AnyAsync(findBy);
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty("The list are empty.");

        return await _table
                .AsNoTracking()
                .ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        if(entity is null)
            throw new ArgumentNullException(nameof(entity));

        _table.Update(entity);
        return entity;
    }
}
