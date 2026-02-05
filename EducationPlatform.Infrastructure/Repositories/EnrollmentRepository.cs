using EducationPlatform.Domain.Entities;
using EducationPlatform.Domain.Interfaces;
using EducationPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EducationPlatform.Infrastructure.Repositories;

public class EnrollmentRepository(EducationPlatformDbContext context) : BaseRepository<EnrollmentsEntity>(context), IEnrollmentRepository
{

    public async Task<EnrollmentsEntity?> GetAsync(Guid participantId, Guid lessonId, CancellationToken cancellationToken)
    {
        return await _table
            .AsNoTracking()
            .Include(s => s.Status)
            .Include(l => l.Lesson)
            .FirstOrDefaultAsync(e => e.ParticipantId == participantId && e.LessonsId == lessonId, cancellationToken);
    }

    public async Task<EnrollmentsEntity?> GetByIdAsync(Guid enrollmentId, CancellationToken cancellationToken)
    {
        return await _table
            .AsNoTracking()
            .Include(s => s.Status)
            .Include(l => l.Lesson)
            .FirstOrDefaultAsync(e => e.Id == enrollmentId, cancellationToken);
    }

    public async Task<IReadOnlyList<EnrollmentsEntity>> GetByLessonAsync(Guid lessonId, CancellationToken cancellationToken)
    {
        return await _table
            .AsNoTracking()
            .Include(s => s.Status)
            .Include(l => l.Lesson)
            .Where(e => e.LessonsId == lessonId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<EnrollmentsEntity>> GetByParticipantAsync(Guid participantId, CancellationToken cancellationToken)
    {
        return await _table
            .AsNoTracking()
            .Include(s => s.Status)
            .Include(l => l.Lesson)
            .Where(e => e.ParticipantId == participantId)
            .ToListAsync(cancellationToken);
    }
}
