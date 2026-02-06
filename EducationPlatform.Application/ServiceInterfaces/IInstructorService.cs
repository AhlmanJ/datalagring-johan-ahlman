using EducationPlatform.Application.DTOs.Expertises;
using EducationPlatform.Application.DTOs.Instructors;

namespace EducationPlatform.Application.ServiceInterfaces;

public interface IInstructorService
{
    Task<InstructorResponseDTO> CreateInstructorAsync(CreateInstructorDTO instructorDTO, CancellationToken cancellationToken);
    Task<InstructorResponseDTO> UpdateInstructorAsync(UpdateInstructorDTO instructorDTO, CancellationToken cancellationToken);
    Task<IReadOnlyList<InstructorResponseDTO>> GetAllInstructorsAsync(CancellationToken cancellationToken);
    Task DeleteInstructorAsync(string instructorEmail, CancellationToken cancellationToken);


    Task<ExpertiseResponseDTO> CreateExpertiseToInstructorAsync(CreateExpertiseDTO expertiseDTO, CancellationToken cancellationToken);
    Task DeleteExpertiseFromInstructorAsync(Guid instructorId, string expertiseSubject, CancellationToken cancellationToken);
}
