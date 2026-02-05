using EducationPlatform.Application.DTOs.Enrollments;
using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Application.Mappers.Enrollments;

public static class EnrollmentMapper
{

    public static EnrollmentResponseDTO ToDTO(EnrollmentsEntity entity)
        => new EnrollmentResponseDTO
         (
            Id: entity.Id,
            ParticipantId: entity.ParticipantId,
            LessonId: entity.LessonsId,
            LessonName: entity.Lesson.Name,
            EnrollmentDate: entity.EnrollmentDate,
            entity.Status.Status
         );

    public static EnrollmentsEntity ToEntity(CreateEnrollmentDTO dto)
        => new EnrollmentsEntity
        {
            ParticipantId = dto.ParticipantId,
            LessonsId = dto.LessonId,
            StatusId = dto.StatusId,
            EnrollmentDate = DateTime.UtcNow
        };
}
