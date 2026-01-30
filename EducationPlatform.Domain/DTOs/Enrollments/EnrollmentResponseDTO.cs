using EducationPlatform.Domain.DTOs.Status;

namespace EducationPlatform.Domain.DTOs.Enrollments;

public sealed record EnrollmentResponseDTO
    (
        Guid Id,
        Guid ParticipantId,
        Guid LessonId,
        DateTime EnrollmentDate,
        string Status
    );
