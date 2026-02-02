using EducationPlatform.Application.DTOs.Instructors;
using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Application.DTOs.Expertises;

public sealed record ExpertiseResponseDTO
    (
        Guid Id,
        InstructorResponseDTO Instructor,
        string Subject = null!
    );
