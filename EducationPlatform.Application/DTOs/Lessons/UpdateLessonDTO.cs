namespace EducationPlatform.Application.DTOs.Lessons;

public sealed record UpdateLessonDTO
(
    string Name,
    DateTime StartDate,
    DateTime EndDate,
    int MaxCapacity,
    string CourseName,
    string Location
);
