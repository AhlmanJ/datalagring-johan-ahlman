namespace EducationPlatform.Domain.DTOs.Participants;

public sealed record UpdateParticipantDTO
    (
        Guid Id,
        string? FirstName,
        string? LastName,
        string? Email,
        Byte[] Concurrency
    );

