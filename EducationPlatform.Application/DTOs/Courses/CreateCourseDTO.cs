using EducationPlatform.Application.DTOs.Lessons;

namespace EducationPlatform.Application.DTOs.Courses;

public sealed record CreateCourseDTO
    (
        string Name,
        string Description
    );