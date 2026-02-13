// A record that is used as a response when a course has been created.

using EducationPlatform.Application.DTOs.Lessons;

namespace EducationPlatform.Application.DTOs.Courses;

public sealed record CourseResponseDTO
    (
        Guid Id,
        string Name,
        String Description
    );

