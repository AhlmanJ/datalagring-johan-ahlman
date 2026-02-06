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
using EducationPlatform.Domain.Interfaces;

namespace EducationPlatform.Application.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EnrollmentService(IEnrollmentRepository enrollmentRepository, IUnitOfWork unitOfWork)
    {
        _enrollmentRepository = enrollmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<EnrollmentResponseDTO> CreateEnrollmentAsync(CreateEnrollmentDTO enrollmentDTO, CancellationToken cancellationToken)
    {
        var savedEnrollment = EnrollmentMapper.ToEntity(enrollmentDTO);
        await _enrollmentRepository.CreateAsync(savedEnrollment, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return EnrollmentMapper.ToDTO(savedEnrollment);
    }

    public async Task<IReadOnlyList<EnrollmentResponseDTO>> GetAllEnrollmentsAsync(CancellationToken cancellationToken)
    {
        var enrollments = await _enrollmentRepository.GetAllAsync(cancellationToken);
        return enrollments.Select(EnrollmentMapper.ToDTO).ToList();
    }

    public async Task DeleteEnrollmentAsync(Guid id, CancellationToken cancellationToken)
    {
        var enrollmentToDelete = await _enrollmentRepository.GetByIdAsync(id, cancellationToken);
        if (enrollmentToDelete == null)
            throw new ArgumentNullException(nameof(enrollmentToDelete));

        await _enrollmentRepository.DeleteAsync(enrollmentToDelete, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}