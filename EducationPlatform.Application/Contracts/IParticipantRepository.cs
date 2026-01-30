using EducationPlatform.Domain.DTOs.Participants;

namespace EducationPlatform.Application.Contracts;

public interface IParticipantRepository
{
    Task<ParticipantResponseDTO> CreateAsync(CreateParticipantDTO Participant, CancellationToken cancellationToken);
    Task<ParticipantResponseDTO?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<ParticipantResponseDTO?> GetByEmailAsync(string Email, CancellationToken cancellationToken);
    Task<IReadOnlyList<ParticipantResponseDTO>> GetAllAsync(CancellationToken cancellationToken);
    Task<ParticipantResponseDTO?> UpdateAsync(UpdateParticipantDTO Participant, CancellationToken cancellationToken);
    Task DeleteByEmailAsync(string Email, CancellationToken cancellationToken);
}
