using EducationPlatform.Domain.Entities;
using EducationPlatform.Domain.Repositories;
using EducationPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EducationPlatform.Infrastructure.Repositories;

public class LessonRepository(EducationPlatformDbContext context) : BaseRepository<LessonsEntity>(context), ILessonRepository
{
    public async Task<LessonsEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _table
            .Include(l => l.Location)
            .Include(c => c.Course)
            .Include(i => i.Instructors)
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<LessonsEntity?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _table
            .Include(l => l.Location)
            .Include(c => c.Course)
            .Include(i => i.Instructors)
            .FirstOrDefaultAsync(l => l.Name == name, cancellationToken);
    }
}
