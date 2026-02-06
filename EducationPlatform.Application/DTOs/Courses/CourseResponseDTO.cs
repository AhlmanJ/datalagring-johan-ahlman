// A record that is used as a response when a course has been created.

namespace EducationPlatform.Application.DTOs.Courses;

public sealed record CourseResponseDTO
    (
        Guid Id,
        string Name,
        String Description
    );

