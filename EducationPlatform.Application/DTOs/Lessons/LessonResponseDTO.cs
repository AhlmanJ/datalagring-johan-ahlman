namespace EducationPlatform.Application.DTOs.Lessons;

public sealed record LessonResponseDTO
    (
        Guid id,
        string Name,
        DateTime StartDate, 
        DateTime EndDate, 
        int MaxCapacity,
        int Enrolled,
        string CourseName,
        string Location
    );
