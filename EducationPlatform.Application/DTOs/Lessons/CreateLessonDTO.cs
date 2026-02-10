namespace EducationPlatform.Application.DTOs.Lessons;

public sealed record CreateLessonDTO
    (
        string Name,
        DateTime StartDate, 
        DateTime EndDate, 
        int MaxCapacity,
        string LocationName
    );
