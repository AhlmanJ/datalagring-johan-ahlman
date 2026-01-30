namespace EducationPlatform.Domain.DTOs.Enrollments;

public sealed record CreateEnrollmentDTO
    (
        Guid ParticipantId,
        Guid LessonId
    );
