using EducationPlatform.Domain.DTOs.Expertises;

namespace EducationPlatform.Domain.DTOs.Instructors;

public sealed record InstructorResponseDTO
    (
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        List<ExpertiseResponseDTO> Expertise
    );
