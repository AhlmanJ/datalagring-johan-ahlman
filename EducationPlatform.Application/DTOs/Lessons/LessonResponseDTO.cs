using EducationPlatform.Application.DTOs.Instructors;
using EducationPlatform.Application.DTOs.Locations;

namespace EducationPlatform.Application.DTOs.Lessons;

public sealed record LessonResponseDTO
    (
        Guid Id,
        string Name,
        DateTime StartDate, 
        DateTime EndDate, 
        int MaxCapacity,
        Guid CourseId,
        string CourseName,
        LocationResponseDTO Location,
        List<InstructorResponseDTO> Instructors
    );
