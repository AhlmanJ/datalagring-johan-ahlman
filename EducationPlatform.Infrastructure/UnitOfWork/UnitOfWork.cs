using EducationPlatform.Application.Abstractions.Persistence;
using EducationPlatform.Infrastructure.Data;

namespace EducationPlatform.Infrastructure.UnitOfWork;

public sealed class UnitOfWork(EducationPlatformDbContext context) : IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken)
        => context.SaveChangesAsync(cancellationToken);
}
