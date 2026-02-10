using EducationPlatform.Domain.Entities;
using EducationPlatform.Domain.Interfaces;
using EducationPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EducationPlatform.Infrastructure.Repositories;

public class ExpertiseRepository(EducationPlatformDbContext context) : BaseRepository<ExpertisesEntity>(context), IExpertiseRepository
{
    public async Task<ExpertisesEntity?> GetByIdAsync(Guid expertiseId, CancellationToken cancellationToken)
    {
        return await _table
            .Include(i => i.Instructors)
            .FirstOrDefaultAsync(e => e.Id == expertiseId, cancellationToken);
    }

    public async Task<ExpertisesEntity?> GetBySubjectAsync(string subject, CancellationToken cancellationToken)
    {
        return await _table
            .Include(i => i.Instructors)
            .FirstOrDefaultAsync(s => s.Subject == subject, cancellationToken);
    }


}
