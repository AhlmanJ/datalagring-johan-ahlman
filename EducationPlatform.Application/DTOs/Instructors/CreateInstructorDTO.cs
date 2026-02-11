namespace EducationPlatform.Application.DTOs.Instructors;

public sealed record CreateInstructorDTO
    (
        string FirstName,
        string LastName,
        string Email
    );
