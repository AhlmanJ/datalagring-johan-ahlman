using EducationPlatform.Application.DTOs.Enrollments;

namespace EducationPlatform.Application.ServiceInterfaces;

public interface IEnrollmentService
{
    Task<EnrollmentResponseDTO> CreateEnrollmentAsync(CreateEnrollmentDTO  enrollmentDTO, Guid paticipantId, Guid lessonsId, CancellationToken cancellationToken);
    Task<bool> DeleteEnrollmentAsync(Guid lessonId, string lessonName, Guid participantId, CancellationToken cancellationToken);
    Task<IReadOnlyList<EnrollmentResponseDTO>> GetAllEnrollmentsAsync(CancellationToken cancellationToken);
}
