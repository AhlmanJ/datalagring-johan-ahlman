namespace EducationPlatform.Application.DTOs.Locations;

public sealed record UpdateLocationDTO
    (
        Guid Id,
        string? Name,
        Byte[] Concurrency
    );
