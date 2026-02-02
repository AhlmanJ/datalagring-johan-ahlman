namespace EducationPlatform.Application.DTOs.Participants;

public sealed record CreateParticipantDTO
    (
        string FirstName,
        string LastName,
        string Email
    );
