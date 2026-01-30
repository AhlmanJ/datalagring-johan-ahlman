/*
 * I got help from chatGPT on how to create this contract/interface.
 * I got help with "breaking down" each code part and had it explained to me what each part does and how I should think.
 * "What information does enrollment need?" "What do I want Enrollment to do?" 
 * The code is partly code created by chatGPT and partly code that I learned by watching the lecture about this in school.
*/
using EducationPlatform.Domain.DTOs.Enrollments;

namespace EducationPlatform.Application.Repositories;

public interface IEnrollmentRepository
{
    Task<EnrollmentResponseDTO?> GetByIdAsync(Guid EnrollmentId, CancellationToken cancellationToken); // Get a specific Enrollment by ID.
    Task<EnrollmentResponseDTO?> GetAsync(Guid ParticipantId, Guid LessonId, CancellationToken cancellationToken); // Get enrollment for a specific participant in a lesson.
    Task<IReadOnlyList<EnrollmentResponseDTO>> GetByParticipantAsync(Guid ParticipantId, CancellationToken cancellationToken); // Get all enrollments for a specific participant by ID.
    Task<IReadOnlyList<EnrollmentResponseDTO>> GetByLessonAsync(Guid LessonId, CancellationToken cancellationToken); // Get all enrollments for a specific lesson by ID.
    Task<bool> ExistsAsync (Guid ParticipantId, Guid LessonId, CancellationToken cancellationToken); // Check if a participant is allready booked on a lesson. 
    Task<EnrollmentResponseDTO> AddAsync(CreateEnrollmentDTO enrollment, CancellationToken cancellationToken); // Add a participant to a lesson.
    Task DeleteAsync(Guid Id, CancellationToken cancellationToken); // Remove a participant from a lesson.
}
