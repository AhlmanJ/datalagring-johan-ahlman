using EducationPlatform.Application.DTOs.Courses;
using EducationPlatform.Application.DTOs.Lessons;
using EducationPlatform.Application.DTOs.Locations;

namespace EducationPlatform.Application.ServiceInterfaces;

public interface ICourseService
{
    Task<CourseResponseDTO> CreateCourseAsync(CreateCourseDTO courseDTO, CancellationToken cancellationToken);
    Task<CourseResponseDTO> UpdateCourseAsync(UpdateCourseDTO courseDTO, CancellationToken cancellationToken);
    Task<IReadOnlyList<CourseResponseDTO>> GetAllCoursesAsync(CancellationToken cancellationToken);
    Task DeleteCourseAsync(string courseName, CancellationToken cancellationToken);

    Task<LessonResponseDTO> CreateLessonToCourseAsync(Guid courseId, CreateLessonDTO lessonDTO, CancellationToken cancellationToken);
    Task DeleteLessonFromCourseAsync(Guid courseId, Guid lessonId , CancellationToken cancellationToken);

    Task<LocationResponseDTO> CreateLocationToCourseAsync(Guid courseId, CreateLocationDTO locationDTO, CancellationToken cancellationToken);
    Task DeleteLocationFromCourseAsync(Guid courseId, string locationName, CancellationToken cancellationToken);
}