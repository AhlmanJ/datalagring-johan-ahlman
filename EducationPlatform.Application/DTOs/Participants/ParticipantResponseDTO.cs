namespace EducationPlatform.Application.DTOs.Participants;

public sealed record ParticipantResponseDTO
    (
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        IReadOnlyList<string>? EnrollmentStatus = null,
        IReadOnlyList<string>? Phonenumber = null
    );
