using EducationPlatform.Domain.Entities;
using System.Linq.Expressions;

namespace EducationPlatform.Domain.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> findBy, CancellationToken cancellationToken);
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
}
