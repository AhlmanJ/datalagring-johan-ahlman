using EducationPlatform.Application.DTOs.Participants;
using EducationPlatform.Application.DTOs.Phonenumbers;

namespace EducationPlatform.Application.ServiceInterfaces;

public interface IParticipantService
{
    Task<ParticipantResponseDTO> CreateParticipantAsync(CreateParticipantDTO participantDTO, CancellationToken cancellationToken);
    Task<ParticipantResponseDTO> UpdateParticipantAsync(string email, UpdateParticipantDTO participantDTO, CancellationToken cancellationToken);
    Task<ParticipantResponseDTO> GetParticipantByEmailAsync(string email, CancellationToken cancellationToken);
    Task<IReadOnlyList<ParticipantResponseDTO>> GetAllParticipantsAsync(CancellationToken cancellationToken);
    Task<bool> DeleteParticipantAsync(string participantName ,CancellationToken cancellationToken);


    Task<PhonenumberResponseDTO> CreatePhonenumberToParticipantAsync(Guid participantId,CreatePhonenumberDTO phonenumberDTO ,CancellationToken cancellationToken);
    Task<bool> DeletePhonenumberFromParticipantAsync(Guid participantId, string phonenumber, CancellationToken cancellationToken);
}
