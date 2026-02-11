using EducationPlatform.Domain.Entities;
using EducationPlatform.Domain.Interfaces;

namespace EducationPlatform.Domain.Repositories;

public interface ILessonRepository : IBaseRepository<LessonsEntity>
{
    Task<LessonsEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<LessonsEntity?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<IReadOnlyList<LessonsEntity>> GetAllByCourseIdAsync(Guid courseId, CancellationToken cancellationToken);
}
