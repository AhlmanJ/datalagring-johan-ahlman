namespace EducationPlatform.Application.DTOs.Enrollments;

public sealed record EnrollmentResponseDTO
    (   
        Guid EnrollmentId,
        string FirstName,
        string LastName,
        string Email,
        string LessonName,
        string LessonLocation,
        DateTime EnrollmentDate,
        DateTime StartDate,
        DateTime EndDate,
        string Status
    );
