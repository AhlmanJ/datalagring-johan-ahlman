namespace EducationPlatform.Application.DTOs.Instructors;

public sealed record InstructorResponseDTO
    (
        Guid InstructorId,
        string FirstName,
        string LastName,
        string Email,
        string Expertise
    );
