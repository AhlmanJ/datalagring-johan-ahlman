using EducationPlatform.Application.DTOs.Instructors;

namespace EducationPlatform.Application.ServiceInterfaces;

public interface IInstructorService
{
    Task<InstructorResponseDTO> CreateInstructorAsync(CreateInstructorDTO instructorDTO, CancellationToken cancellationToken);
    Task<InstructorResponseDTO> UpdateInstructorAsync(Guid id, UpdateInstructorDTO instructorDTO, CancellationToken cancellationToken);
    Task<IReadOnlyList<InstructorResponseDTO>> GetAllInstructorsAsync(CancellationToken cancellationToken);
    Task<InstructorResponseDTO> GetInstructorByEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> DeleteInstructorAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> EnrollInstructorToLessonAsync(Guid lessonId, Guid instructorId, CancellationToken cancellationToken);
}
