using EducationPlatform.Application.DTOs.Courses;
using EducationPlatform.Application.DTOs.Lessons;
using EducationPlatform.Application.DTOs.Locations;

namespace EducationPlatform.Application.ServiceInterfaces;

public interface ICourseService
{
    Task<CourseResponseDTO> CreateCourseAsync(CreateCourseDTO courseDTO, string Name, CancellationToken cancellationToken);
    Task<CourseResponseDTO> UpdateCourseAsync(string Name, UpdateCourseDTO courseDTO, CancellationToken cancellationToken);
    Task<IReadOnlyList<CourseResponseDTO>> GetAllCoursesAsync(CancellationToken cancellationToken);
    Task<bool> DeleteCourseAsync(Guid id, CancellationToken cancellationToken);

    Task<LessonResponseDTO> CreateLessonToCourseAsync(Guid courseId, CreateLessonDTO lessonDTO, CancellationToken cancellationToken);
    Task<bool> DeleteLessonFromCourseAsync(Guid courseId, Guid lessonId , CancellationToken cancellationToken);

    Task<LocationResponseDTO> CreateLocationToCourseAsync(Guid courseId,string lessonName, CreateLocationDTO locationDTO, CancellationToken cancellationToken);
    Task<bool> DeleteLocationFromCourseAsync(Guid courseId, string locationName, CancellationToken cancellationToken);
}