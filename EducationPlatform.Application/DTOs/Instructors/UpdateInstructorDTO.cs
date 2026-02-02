namespace EducationPlatform.Application.DTOs.Instructors;

public sealed record UpdateInstructorDTO
    (
        Guid Id,
        string? FirstName,
        string? LastName,
        string? Email,
        Byte[] Concurrency
    );
