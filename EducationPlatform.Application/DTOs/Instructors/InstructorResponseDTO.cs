using EducationPlatform.Application.DTOs.Expertises;

namespace EducationPlatform.Application.DTOs.Instructors;

public sealed record InstructorResponseDTO
    (
        Guid Id,
        string FirstName,
        string LastName,
        string Email
    );
