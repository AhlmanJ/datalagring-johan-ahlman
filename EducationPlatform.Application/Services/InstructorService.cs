
// See notes in the code for "CreateExpertiseToInstructorAsync", row 91.

using EducationPlatform.Application.Abstractions.Persistence;
using EducationPlatform.Application.DTOs.Instructors;
using EducationPlatform.Application.Mappers.Instructors;
using EducationPlatform.Application.ServiceInterfaces;
using EducationPlatform.Domain.Interfaces;
using EducationPlatform.Domain.Repositories;

namespace EducationPlatform.Application.Services;

public class InstructorService : IInstructorService
{
    private readonly IInstructorRepository _instructorRepository;
    private readonly ILessonRepository _lessonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InstructorService(IInstructorRepository instructorRepository,ILessonRepository lessonRepository, IUnitOfWork unitOfWork)
    {
        _instructorRepository = instructorRepository;
        _lessonRepository = lessonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<InstructorResponseDTO> CreateInstructorAsync(CreateInstructorDTO instructorDTO, CancellationToken cancellationToken)
    {
        if (instructorDTO == null)
            throw new ArgumentException("Instructor cannot be empty. Please try again.");

        var allreadyExists = await _instructorRepository.ExistsAsync(e => e.Email == instructorDTO.Email, cancellationToken);
        if (allreadyExists)
            throw new ArgumentException("Instructor allready exist. Please try again.");


        var savedInstructor = InstructorMapper.ToEntity(instructorDTO);
        await _instructorRepository.CreateAsync(savedInstructor, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return InstructorMapper.ToDTO(savedInstructor);
    }

    public async Task<InstructorResponseDTO> GetInstructorByEmailAsync(string email, CancellationToken cancellationToken)
    {
        if(email == null)
            throw new ArgumentException("Email cannot be empty. Please try again.");

        var instructor = await _instructorRepository.GetByEmailAsync(email, cancellationToken);
        if (instructor == null) 
            throw new KeyNotFoundException($"Could not find a Instructor with Email address {email}. Please try again.");

        return InstructorMapper.ToDTO(instructor);
    }

    public async Task<bool> EnrollInstructorToLessonAsync(Guid lessonId, Guid instructorId, CancellationToken cancellationToken)
    {
        if (lessonId == Guid.Empty)
            throw new ArgumentNullException(nameof(lessonId));

        if(instructorId == Guid.Empty)
            throw new ArgumentNullException(nameof(instructorId));

        var lesson = await _lessonRepository.GetByIdAsync(lessonId, cancellationToken);
            if(lesson == null)
            return false;

        var instructor = await _instructorRepository.GetByIdAsync(instructorId, cancellationToken);
            if(instructor == null)
            return false;

        lesson.Instructors.Add(instructor);

        await _lessonRepository.UpdateAsync(lesson, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return true;
    }   

    public async Task<IReadOnlyList<InstructorResponseDTO>> GetAllInstructorsAsync(CancellationToken cancellationToken)
    {
        var instructors = await _instructorRepository.GetAllAsync(cancellationToken);
        if(instructors.Count == 0)
            return new List<InstructorResponseDTO>();

        return instructors.Select(InstructorMapper.ToDTO).ToList();
    }

    public async Task<InstructorResponseDTO> UpdateInstructorAsync(Guid id, UpdateInstructorDTO instructorDTO, CancellationToken cancellationToken)
    {
        if(instructorDTO == null)
            throw new ArgumentNullException(nameof(instructorDTO));

        if(id == Guid.Empty)
            throw new ArgumentNullException(nameof(id));

        var instructorToUpdate = await _instructorRepository.GetByIdAsync(id, cancellationToken);
        if (instructorToUpdate == null)
            throw new KeyNotFoundException($"Could not find a instructor with Id: {id}");

        InstructorMapper.UpdateEntity(instructorToUpdate, instructorDTO);
        await _unitOfWork.CommitAsync(cancellationToken);

        return InstructorMapper.ToDTO(instructorToUpdate);
    }

    public async Task<bool> DeleteInstructorAsync(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullException(nameof(id));

        var instructorToDelete = await _instructorRepository.GetByIdAsync(id, cancellationToken);
        if (instructorToDelete == null)
            return false;

        await _instructorRepository.DeleteAsync(instructorToDelete, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return true;
    }
}
