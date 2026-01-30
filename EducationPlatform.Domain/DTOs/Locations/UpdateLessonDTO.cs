namespace EducationPlatform.Domain.DTOs.Locations;

public sealed record UpdateLessonDTO
    (
        Guid Id,
        string? Name,
        Byte[] Concurrency
    );
