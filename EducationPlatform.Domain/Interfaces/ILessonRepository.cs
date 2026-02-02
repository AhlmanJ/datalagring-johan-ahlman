using EducationPlatform.Domain.Entities;
using EducationPlatform.Domain.Interfaces;

namespace EducationPlatform.Domain.Repositories;

public interface ILessonRepository : IBaseRepository<LessonsEntity>
{
    Task<LessonsEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<LessonsEntity?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<LessonsEntity?> UpdateAsync(LessonsEntity course, CancellationToken cancellationToken);
    Task DeleteAsync(LessonsEntity course, CancellationToken cancellationToken);
}
