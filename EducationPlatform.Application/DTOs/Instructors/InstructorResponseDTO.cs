namespace EducationPlatform.Application.DTOs.Instructors;

public sealed record InstructorResponseDTO
    (
        string FirstName,
        string LastName,
        string Email,
        IReadOnlyList<string>? Subject = null
    );
