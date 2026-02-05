using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Domain.Interfaces;

public interface IPhonenumberRepository : IBaseRepository<PhonenumbersEntity>
{
    Task<IReadOnlyList<PhonenumbersEntity>> GetByParticipantAsync(Guid participantId, CancellationToken cancellationToken);
}
