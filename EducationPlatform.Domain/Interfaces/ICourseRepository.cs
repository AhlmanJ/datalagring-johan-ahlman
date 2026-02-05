using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Domain.Interfaces;

public interface ICourseRepository : IBaseRepository<CoursesEntity>
{
    Task<CoursesEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<CoursesEntity?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
