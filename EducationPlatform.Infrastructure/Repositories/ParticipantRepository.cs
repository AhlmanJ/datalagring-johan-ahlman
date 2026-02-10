using EducationPlatform.Domain.Entities;
using EducationPlatform.Domain.Interfaces;
using EducationPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EducationPlatform.Infrastructure.Repositories;

public class ParticipantRepository(EducationPlatformDbContext context) : BaseRepository<ParticipantsEntity>(context), IParticipantRepository
{
    public async Task<ParticipantsEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _table
            .Include(p => p.Phonenumbers)
            .Include(e => e.Enrollments)
            .FirstOrDefaultAsync(p => p.Email == email, cancellationToken);
    }

    public async Task<ParticipantsEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _table
            .Include(p => p.Phonenumbers)
            .Include(e => e.Enrollments)
                .ThenInclude(l => l.Lesson)
            .Include(e => e.Enrollments)
                .ThenInclude(s => s.Status)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}
