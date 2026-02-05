namespace EducationPlatform.Application.DTOs.Enrollments;

public sealed record CreateEnrollmentDTO
    (
        Guid ParticipantId,
        Guid LessonId,
        string LessonName,
        Guid StatusId
    );
