using EducationPlatform.Domain.Entities;
using EducationPlatform.Domain.Interfaces;
using EducationPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EducationPlatform.Infrastructure.Repositories;

public class CourseRepository(EducationPlatformDbContext context) : BaseRepository<CoursesEntity>(context), ICourseRepository
{
    public async Task<CoursesEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _table
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<CoursesEntity?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _table
            .FirstOrDefaultAsync(c => c.Name == name, cancellationToken);
    }
}
