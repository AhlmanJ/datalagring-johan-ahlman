using EducationPlatform.Application.DTOs.Participants;
using EducationPlatform.Application.DTOs.Phonenumbers;

namespace EducationPlatform.Application.ServiceInterfaces;

public interface IParticipantService
{
    Task<ParticipantResponseDTO> CreateParticipantAsync(CreateParticipantDTO participantDTO, CancellationToken cancellationToken);
    Task<ParticipantResponseDTO> UpdateParticipantAsync(UpdateParticipantDTO participantDTO, CancellationToken cancellationToken);
    Task<ParticipantResponseDTO> GetParticipantByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ParticipantResponseDTO>> GetAllParticipantsAsync(CancellationToken cancellationToken);
    Task DeleteParticipantAsync(string participantName ,CancellationToken cancellationToken);


    Task<PhonenumberResponseDTO> CreatePhonenumberToParticipantAsync(Guid participantId,CreatePhonenumberDTO phonenumberDTO ,CancellationToken cancellationToken);
    Task DeletePhonenumberFromParticipantAsync(Guid participantId, Guid phonenumberId, CancellationToken cancellationToken);
}
