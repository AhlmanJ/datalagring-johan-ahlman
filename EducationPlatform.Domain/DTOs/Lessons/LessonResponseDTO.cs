using EducationPlatform.Domain.DTOs.Instructors;
using EducationPlatform.Domain.DTOs.Locations;

namespace EducationPlatform.Domain.DTOs.Lessons;

public sealed record LessonResponseDTO
    (
        Guid Id,
        string Name,
        DateTime StartDate, 
        DateTime EndDate, 
        int MaxCapacity, 
        LocationResponseDTO Location,
        List<InstructorResponseDTO> Instructors
    );
