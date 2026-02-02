using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Domain.Interfaces;

public interface IExpertiseRepository : IBaseRepository<ExpertisesEntity>
{
    Task<ExpertisesEntity?> GetBySubjectAsync(string subject, CancellationToken cancellationToken);
}
