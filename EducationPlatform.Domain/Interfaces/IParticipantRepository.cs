using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Domain.Interfaces;

public interface IParticipantRepository : IBaseRepository<ParticipantsEntity>
{
    Task<ParticipantsEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ParticipantsEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}
