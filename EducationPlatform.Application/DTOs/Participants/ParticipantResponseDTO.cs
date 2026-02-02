namespace EducationPlatform.Application.DTOs.Participants;

public sealed record ParticipantResponseDTO
    (
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        IReadOnlyList<string>? Phonenumber = null
    );
