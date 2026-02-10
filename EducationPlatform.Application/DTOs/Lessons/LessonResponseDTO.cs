namespace EducationPlatform.Application.DTOs.Lessons;

public sealed record LessonResponseDTO
    (
        string Name,
        DateTime StartDate, 
        DateTime EndDate, 
        int MaxCapacity,
        string CourseName,
        string Location
    );
