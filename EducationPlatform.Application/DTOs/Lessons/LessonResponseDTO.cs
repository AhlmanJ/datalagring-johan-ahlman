namespace EducationPlatform.Application.DTOs.Lessons;

public sealed record LessonResponseDTO
    (
        Guid id,
        string Name,
        DateTime StartDate, 
        DateTime EndDate, 
        int MaxCapacity,
        int Enrolled,
        string Location,
        IReadOnlyList<string>? Instructors = null
    );
