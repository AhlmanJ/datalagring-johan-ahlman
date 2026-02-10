using EducationPlatform.Application.DTOs.Expertises;
using EducationPlatform.Application.DTOs.Instructors;

namespace EducationPlatform.Application.ServiceInterfaces;

public interface IInstructorService
{
    Task<InstructorResponseDTO> CreateInstructorAsync(CreateInstructorDTO instructorDTO, CancellationToken cancellationToken);
    Task<InstructorResponseDTO> UpdateInstructorAsync(Guid id, UpdateInstructorDTO instructorDTO, CancellationToken cancellationToken);
    Task<IReadOnlyList<AllInstructorsResponseDTO>> GetAllInstructorsAsync(CancellationToken cancellationToken);
    Task<bool> DeleteInstructorAsync(Guid id, CancellationToken cancellationToken);


    Task<ExpertiseResponseDTO> CreateExpertiseToInstructorAsync(Guid instructorId, CreateExpertiseDTO expertiseDTO, CancellationToken cancellationToken);
    Task<bool> DeleteExpertiseFromInstructorAsync(Guid instructorId, Guid expertiseId, CancellationToken cancellationToken);
}
