// A record that is used as a response when a course has been created.

using EducationPlatform.Domain.DTOs.Lessons;

namespace EducationPlatform.Domain.DTOs.Courses;

public sealed record CourseResponseDTO
    (
        Guid Id,
        string Name,
        String Description,
        List<LessonResponseDTO> Lessons
    );

