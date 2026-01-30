using EducationPlatform.Domain.DTOs.Courses;
using EducationPlatform.Domain.DTOs.Lessons;

namespace EducationPlatform.Application.Repositories;

public interface ILessonRepository
{
    Task<LessonResponseDTO> CreateLessonAsync(CreateLessonDTO Lesson, CancellationToken cancellationToken);
    Task<LessonResponseDTO?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<LessonResponseDTO?> GetByNameAsync(string Name, CancellationToken cancellationToken);
    Task<IReadOnlyList<LessonResponseDTO>> GetAllAsync(CancellationToken cancellationToken);
    Task<LessonResponseDTO?> UpdateAsync(UpdateCourseDTO course, CancellationToken cancellationToken);
    Task DeleteByNameAsync(string Name, CancellationToken cancellationToken);
}
