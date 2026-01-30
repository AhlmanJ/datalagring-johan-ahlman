using EducationPlatform.Domain.DTOs.Instructors;

namespace EducationPlatform.Application.Contracts;

public interface IInstructorRepository
{
    Task<InstructorResponseDTO> CreateAsync(CreateInstructorDTO Instructor, CancellationToken cancellationToken);
    Task<InstructorResponseDTO?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<InstructorResponseDTO?> GetByEmailAsync(string Email, CancellationToken cancellationToken);
    Task<IReadOnlyList<InstructorResponseDTO>> GetAllAsync(CancellationToken cancellationToken);
    Task<InstructorResponseDTO?> UpdateAsync(UpdateInstructorDTO Instructor, CancellationToken cancellationToken);
    Task DeleteByEmailAsync(string Email, CancellationToken cancellationToken);
}
