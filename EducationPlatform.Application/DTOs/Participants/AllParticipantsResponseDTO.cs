namespace EducationPlatform.Application.DTOs.Participants;

public sealed record AllParticipantsResponseDTO
(   
    Guid ParticipantId,
    string FirstName,
    string LastName,
    string Email
);
