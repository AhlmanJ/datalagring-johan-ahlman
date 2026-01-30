namespace EducationPlatform.Domain.DTOs.Instructors;

public sealed record UpdateInstructorDTO
    (
        Guid Id,
        string? FirstName,
        string? LastName,
        string? Email,
        Byte[] Concurrency
    );
