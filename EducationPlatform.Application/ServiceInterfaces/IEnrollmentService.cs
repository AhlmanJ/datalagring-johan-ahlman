using EducationPlatform.Application.DTOs.Enrollments;

namespace EducationPlatform.Application.ServiceInterfaces;

public interface IEnrollmentService
{
    Task<EnrollmentResponseDTO> CreateEnrollmentAsync(CreateEnrollmentDTO  enrollmentDTO, Guid paticipantId, Guid lessonsId, CancellationToken cancellationToken);
    Task<bool> DeleteEnrollmentAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<EnrollmentResponseDTO>> GetAllEnrollmentsAsync(CancellationToken cancellationToken);
}
