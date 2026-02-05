using EducationPlatform.Domain.Entities;
using EducationPlatform.Domain.Interfaces;
using EducationPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EducationPlatform.Infrastructure.Repositories;

public class PhonenumberRepository(EducationPlatformDbContext context) : BaseRepository<PhonenumbersEntity>(context), IPhonenumberRepository
{
    public async Task<IReadOnlyList<PhonenumbersEntity>> GetByParticipantAsync(Guid participantId, CancellationToken cancellationToken)
    {
        return await _table
            .AsNoTracking()
            .Where(p => p.ParticipantId == participantId)
            .ToListAsync(cancellationToken);
    }
}
