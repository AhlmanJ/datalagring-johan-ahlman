namespace EducationPlatform.Application.DTOs.Lessons;

public sealed record CreateLessonDTO
    (
        Guid CourseId, // FK
        string Name,
        DateTime StartDate, 
        DateTime EndDate, 
        int MaxCapacity
    );
