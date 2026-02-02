/*
 * I got help from chatGPT on how to create this contract/interface.
 * I got help with "breaking down" each code part and had it explained to me what each part does and how I should think.
 * "What information does enrollment need?" "What do I want Enrollment to do?" 
 * The code is partly code created by chatGPT and partly code that I learned by watching the lecture about this in school.
*/
using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Domain.Interfaces;

public interface IEnrollmentRepository : IBaseRepository<EnrollmentsEntity>
{
    Task<EnrollmentsEntity?> GetByIdAsync(Guid enrollmentId, CancellationToken cancellationToken); // Get a specific Enrollment by ID.
    Task<EnrollmentsEntity?> GetAsync(Guid participantId, Guid lessonId, CancellationToken cancellationToken); // Get enrollment for a specific participant in a lesson.
    Task<IReadOnlyList<EnrollmentsEntity>> GetByParticipantAsync(Guid participantId, CancellationToken cancellationToken); // Get all enrollments for a specific participant by ID.
    Task<IReadOnlyList<EnrollmentsEntity>> GetByLessonAsync(Guid lessonId, CancellationToken cancellationToken); // Get all enrollments for a specific lesson by ID.
    Task<bool> ExistsAsync (Guid participantId, Guid lessonId, CancellationToken cancellationToken); // Check if a participant is allready booked on a lesson. 
    Task<EnrollmentsEntity> AddAsync(EnrollmentsEntity enrollment, CancellationToken cancellationToken); // Add a participant to a lesson.
    Task DeleteAsync(EnrollmentsEntity enrollment, CancellationToken cancellationToken); // Remove a participant from a lesson.
}
