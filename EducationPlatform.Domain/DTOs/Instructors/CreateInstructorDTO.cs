using EducationPlatform.Domain.DTOs.Expertises;

namespace EducationPlatform.Domain.DTOs.Instructors;

public sealed record CreateInstructorDTO
    (
        string FirstName,
        string LastName,
        string Email,
        ExpertiseResponseDTO Expertise
    );
