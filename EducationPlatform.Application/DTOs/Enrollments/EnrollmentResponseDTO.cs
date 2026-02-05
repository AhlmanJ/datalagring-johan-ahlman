namespace EducationPlatform.Application.DTOs.Enrollments;

public sealed record EnrollmentResponseDTO
    (
        Guid Id,
        Guid ParticipantId,
        Guid LessonId,
        string LessonName,
        DateTime EnrollmentDate,
        string Status
    );
