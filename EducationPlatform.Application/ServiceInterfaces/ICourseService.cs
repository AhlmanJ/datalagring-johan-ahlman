using EducationPlatform.Application.DTOs.Courses;
using EducationPlatform.Application.DTOs.Lessons;
using EducationPlatform.Application.DTOs.Locations;

namespace EducationPlatform.Application.ServiceInterfaces;

public interface ICourseService
{
    Task<CourseResponseDTO> CreateCourseAsync(CreateCourseDTO courseDTO, CancellationToken cancellationToken);
    Task<CourseResponseDTO> UpdateCourseAsync(string name, UpdateCourseDTO courseDTO, CancellationToken cancellationToken);
    Task<IReadOnlyList<CourseResponseDTO>> GetAllCoursesAsync(CancellationToken cancellationToken);
    Task<bool> DeleteCourseAsync(Guid id, CancellationToken cancellationToken);



    Task<LessonResponseDTO> CreateLessonToCourseAsync(Guid courseId, CreateLessonDTO lessonDTO, CancellationToken cancellationToken);
    Task<IReadOnlyList<LessonResponseDTO>> GetAllLessonByCourseIdAsync(Guid courseId, CancellationToken cancellationToken);
    Task<LessonResponseDTO> UpdateLessonAsync(string name, UpdateLessonDTO updateLessonDTO, CancellationToken cancellationToken);
    Task<bool> DeleteLessonFromCourseAsync(Guid courseId, Guid lessonId , CancellationToken cancellationToken);



    Task<IReadOnlyList<LocationResponseDTO>> GetAllLocationsAsync(CancellationToken cancellationToken);
    Task<bool> DeleteLocationFromCourseAsync(string locationName, CancellationToken cancellationToken);
}