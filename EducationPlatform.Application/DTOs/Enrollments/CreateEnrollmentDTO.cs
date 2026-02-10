namespace EducationPlatform.Application.DTOs.Enrollments;

public sealed record CreateEnrollmentDTO
    (
        Guid ParticipantId,
        Guid LessonsId
    );
