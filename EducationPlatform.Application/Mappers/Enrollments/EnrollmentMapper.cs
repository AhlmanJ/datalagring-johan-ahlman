using EducationPlatform.Application.DTOs.Enrollments;
using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Application.Mappers.Enrollments;

public static class EnrollmentMapper
{
    public static EnrollmentResponseDTO ToDTO(EnrollmentsEntity entity)
        => new EnrollmentResponseDTO
         (
            EnrollmentId: entity.Id,
            FirstName: entity.Participant.FirstName,
            LastName: entity.Participant.LastName,
            Email: entity.Participant.Email,
            LessonName: entity.Lesson.Name,
            LessonLocation: entity.Lesson.Location.Name,
            EnrollmentDate: entity.EnrollmentDate,
            StartDate: entity.Lesson.StartDate,
            EndDate: entity.Lesson.EndDate,
            Status: "Booked"
         );

    public static EnrollmentsEntity ToEntity(CreateEnrollmentDTO dto)
        => new EnrollmentsEntity
        {
            ParticipantId = dto.ParticipantId,
            LessonsId = dto.LessonsId
        };
}
