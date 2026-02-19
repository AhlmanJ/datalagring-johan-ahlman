
// I learned how to create a Unit of work by watching a lecture at school.

using EducationPlatform.Application.Abstractions.Persistence;
using EducationPlatform.Infrastructure.Data;

namespace EducationPlatform.Infrastructure.UnitOfWork;

public sealed class UnitOfWork(EducationPlatformDbContext context) : IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken)
        => context.SaveChangesAsync(cancellationToken);
}
