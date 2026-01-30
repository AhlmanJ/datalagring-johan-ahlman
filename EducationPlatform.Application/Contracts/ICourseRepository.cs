using EducationPlatform.Domain.DTOs.Courses;

namespace EducationPlatform.Application.Repositories;

public interface ICourseRepository
{
    Task<CourseResponseDTO> CreateAsync(CreateCourseDTO course, CancellationToken cancellationToken);
    Task<CourseResponseDTO?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<CourseResponseDTO?> GetByNameAsync(string Name, CancellationToken cancellationToken);
    Task<IReadOnlyList<CourseResponseDTO>> GetAllAsync(CancellationToken cancellationToken);
    Task<CourseResponseDTO?> UpdateAsync(UpdateCourseDTO course, CancellationToken cancellationToken);
    Task DeleteByNameAsync(string Name, CancellationToken cancellationToken);
}
