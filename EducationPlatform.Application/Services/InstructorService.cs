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

    public async Task<IReadOnlyList<InstructorResponseDTO>> GetAllInstructorsAsync(CancellationToken cancellationToken)
    {
        var instructors = await _instructorRepository.GetAllAsync(cancellationToken);
        return instructors.Select(InstructorMapper.ToDTO).ToList();
    }

    public async Task<InstructorResponseDTO> UpdateInstructorAsync(UpdateInstructorDTO instructorDTO, CancellationToken cancellationToken)
    {
        if(instructorDTO == null)
            throw new ArgumentNullException(nameof(instructorDTO));

        var instructorToUpdate = await _instructorRepository.GetByEmailAsync(instructorDTO.Email!, cancellationToken);
        if (instructorToUpdate == null)
            throw new ArgumentNullException(nameof(instructorToUpdate));

        InstructorMapper.UpdateEntity(instructorToUpdate, instructorDTO);
        await _unitOfWork.CommitAsync(cancellationToken);

        return InstructorMapper.ToDTO(instructorToUpdate);
    }

    public async Task DeleteInstructorAsync(string instructorEmail, CancellationToken cancellationToken)
    {
        var instructorToDelete = await _instructorRepository.GetByEmailAsync(instructorEmail, cancellationToken);
        if (instructorToDelete == null)
            throw new ArgumentNullException(nameof(instructorToDelete));

        await _instructorRepository.DeleteAsync(instructorToDelete, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }




    public async Task<ExpertiseResponseDTO> CreateExpertiseToInstructorAsync(CreateExpertiseDTO expertiseDTO, CancellationToken cancellationToken)
    {
        var savedExpertise = ExpertiseMapper.ToEntity(expertiseDTO);
        await _expertiseRepository.CreateAsync(savedExpertise, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return ExpertiseMapper.ToDTO(savedExpertise);
    }

    public async Task DeleteExpertiseFromInstructorAsync(Guid instructorId, string expertiseSubject, CancellationToken cancellationToken)
    {
        var expertiseToDelete = await _expertiseRepository.GetBySubjectAsync(expertiseSubject, cancellationToken);
        if(expertiseToDelete == null)
            throw new ArgumentNullException(nameof(expertiseToDelete));

        await _expertiseRepository.DeleteAsync(expertiseToDelete, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}
