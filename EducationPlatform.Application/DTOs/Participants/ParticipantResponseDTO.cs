namespace EducationPlatform.Application.DTOs.Participants;

public sealed record ParticipantResponseDTO
    (
        Guid ParticipantId,
        string FirstName,
        string LastName,
        string Email,
        IReadOnlyList<string>? Phonenumber = null
    );
