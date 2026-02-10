using EducationPlatform.Application.Abstractions.Persistence;
using EducationPlatform.Application.DTOs.Expertises;
using EducationPlatform.Application.DTOs.Instructors;
using EducationPlatform.Application.Mappers.Expertises;
using EducationPlatform.Application.Mappers.Instructors;
using EducationPlatform.Application.ServiceInterfaces;
using EducationPlatform.Domain.Interfaces;

namespace EducationPlatform.Application.Services;

public class InstructorService : IInstructorService
{
    private readonly IInstructorRepository _instructorRepository;
    private readonly IExpertiseRepository _expertiseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InstructorService(IInstructorRepository instructorRepository, IExpertiseRepository expertiseRepository, IUnitOfWork unitOfWork)
    {
        _instructorRepository = instructorRepository;
        _expertiseRepository = expertiseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<InstructorResponseDTO> CreateInstructorAsync(CreateInstructorDTO instructorDTO, CancellationToken cancellationToken)
    {
        var savedInstructor = InstructorMapper.ToEntity(instructorDTO);
        await _instructorRepository.CreateAsync(savedInstructor, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return InstructorMapper.ToDTO(savedInstructor);
    }

    public async Task<IReadOnlyList<AllInstructorsResponseDTO>> GetAllInstructorsAsync(CancellationToken cancellationToken)
    {
        var instructors = await _instructorRepository.GetAllAsync(cancellationToken);
        return instructors.Select(InstructorMapper.AllToDTO).ToList();
    }

    public async Task<InstructorResponseDTO> UpdateInstructorAsync(Guid id, UpdateInstructorDTO instructorDTO, CancellationToken cancellationToken)
    {
        if(instructorDTO == null)
            throw new ArgumentNullException(nameof(instructorDTO));

        var instructorToUpdate = await _instructorRepository.GetByIdAsync(id, cancellationToken);
        if (instructorToUpdate == null)
            throw new ArgumentNullException(nameof(instructorToUpdate));

        InstructorMapper.UpdateEntity(instructorToUpdate, instructorDTO);
        await _unitOfWork.CommitAsync(cancellationToken);

        return InstructorMapper.ToDTO(instructorToUpdate);
    }

    public async Task<bool> DeleteInstructorAsync(Guid id, CancellationToken cancellationToken)
    {
        var instructorToDelete = await _instructorRepository.GetByIdAsync(id, cancellationToken);
        if (instructorToDelete == null)
            return false;

        await _instructorRepository.DeleteAsync(instructorToDelete, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return true;
    }




    public async Task<ExpertiseResponseDTO> CreateExpertiseToInstructorAsync(Guid instructorId, CreateExpertiseDTO expertiseDTO, CancellationToken cancellationToken)
    {
        var instructor = await _instructorRepository.GetByIdAsync(instructorId, cancellationToken);
        if (instructor == null)
            throw new ArgumentNullException("The instructor does not exist.");

        var savedExpertise = ExpertiseMapper.ToEntity(expertiseDTO);
        await _expertiseRepository.CreateAsync(savedExpertise, cancellationToken);

        instructor.Expertises.Add(savedExpertise); // Explained to me by chatGPT that EF Core needs to know the many-to-many relation. Otherwise the expertise will be created but not related to the instructor.
        await _unitOfWork.CommitAsync(cancellationToken);

        return ExpertiseMapper.ToDTO(savedExpertise);
    }

    public async Task<bool> DeleteExpertiseFromInstructorAsync(Guid instructorId, Guid id, CancellationToken cancellationToken)
    {
        var expertiseToDelete = await _expertiseRepository.GetByIdAsync(id, cancellationToken);
        if(expertiseToDelete == null)
            return false;

        await _expertiseRepository.DeleteAsync(expertiseToDelete, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return true;
    }
}
