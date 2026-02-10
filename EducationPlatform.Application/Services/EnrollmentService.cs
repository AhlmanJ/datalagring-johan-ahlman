/*
 * I got help from chatGPT on how to create this service.
 * I got help with "breaking down" each code part and had it explained to me what each part does and how I should think.
 * "What do I want Service to do?", "How does the service relate DOTs?" etc... 
 * The code is partly code created by chatGPT and partly code that I learned by watching the lecture about this in school.
*/

using EducationPlatform.Application.Abstractions.Persistence;
using EducationPlatform.Application.DTOs.Enrollments;
using EducationPlatform.Application.Mappers.Enrollments;
using EducationPlatform.Application.ServiceInterfaces;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Domain.Interfaces;
using EducationPlatform.Domain.Repositories;

namespace EducationPlatform.Application.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IParticipantRepository _participantRepository;
    private readonly ILessonRepository _lessonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EnrollmentService(IEnrollmentRepository enrollmentRepository, IParticipantRepository participantRepository , ILessonRepository lessonRepository, IUnitOfWork unitOfWork)
    {
        _enrollmentRepository = enrollmentRepository;
        _participantRepository = participantRepository;
        _lessonRepository = lessonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<EnrollmentResponseDTO> CreateEnrollmentAsync(CreateEnrollmentDTO enrollmentDTO, Guid participantId, Guid lessonsId, CancellationToken cancellationToken)
    {
        var participant = await _participantRepository.GetByIdAsync(participantId, cancellationToken);
        if(participant == null) 
            throw new ArgumentNullException(nameof(participant), "No participant found");
        
        var lesson = await _lessonRepository.GetByIdAsync(lessonsId, cancellationToken);
        if(lesson == null)
            throw new ArgumentNullException($"{lessonsId} does not exists.");

        var savedEnrollment = EnrollmentMapper.ToEntity(enrollmentDTO);
        savedEnrollment.Participant = participant;
        savedEnrollment.Lesson = lesson;
        savedEnrollment.Lesson.Location = lesson.Location;
        savedEnrollment.StatusId = Guid.Parse("22222222-2222-2222-2222-222222222222"); // I got help from chatGPT on how to override the status i the mapper and set the status "Booked" that i have in my DbContext.(I don't really need the status in the DTO if I do this.)
        savedEnrollment.EnrollmentDate = DateTime.UtcNow; // Make shure that the enrollment date is correct.
       
        
        
        await _enrollmentRepository.CreateAsync(savedEnrollment, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return EnrollmentMapper.ToDTO(savedEnrollment);
    }

    public async Task<IReadOnlyList<EnrollmentResponseDTO>> GetAllEnrollmentsAsync(CancellationToken cancellationToken)
    {
        var enrollments = await _enrollmentRepository.GetAllEnrollmentsAsync(cancellationToken);
        return enrollments.Select(EnrollmentMapper.ToDTO).ToList();
    }

    public async Task<bool> DeleteEnrollmentAsync(Guid id, CancellationToken cancellationToken)
    {
        var enrollmentToDelete = await _enrollmentRepository.GetByIdAsync(id, cancellationToken);
        if (enrollmentToDelete == null)
            return false;

        await _enrollmentRepository.DeleteAsync(enrollmentToDelete, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return true;
    }
}