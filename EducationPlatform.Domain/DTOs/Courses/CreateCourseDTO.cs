namespace EducationPlatform.Domain.DTOs.Courses;

public sealed record CreateCourseDTO
    (
        string Name,
        string Description
    );
