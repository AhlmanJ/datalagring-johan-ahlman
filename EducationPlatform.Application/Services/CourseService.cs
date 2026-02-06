
// I got support from chatGPT with the method UpdateCourseAsync. I needed help to explain how to do this.

using EducationPlatform.Application.Abstractions.Persistence;
using EducationPlatform.Application.DTOs.Courses;
using EducationPlatform.Application.DTOs.Lessons;
using EducationPlatform.Application.DTOs.Locations;
using EducationPlatform.Application.Mappers.Courses;
using EducationPlatform.Application.Mappers.Lessons;
using EducationPlatform.Application.Mappers.Locations;
using EducationPlatform.Application.ServiceInterfaces;
using EducationPlatform.Domain.Interfaces;
using EducationPlatform.Domain.Repositories;

namespace EducationPlatform.Application.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly ILessonRepository _lessonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CourseService(ICourseRepository courseRepository,ILocationRepository locationRepository, ILessonRepository lessonRepository, IUnitOfWork unitOfWork)
    {  _courseRepository = courseRepository;
       _locationRepository = locationRepository;
       _lessonRepository = lessonRepository;
       _unitOfWork = unitOfWork;
    }

    public async Task<CourseResponseDTO> CreateCourseAsync(CreateCourseDTO courseDTO, CancellationToken cancellationToken)
    {
        var savedCourse = CourseMapper.ToEntity(courseDTO);
        await _courseRepository.CreateAsync(savedCourse, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return CourseMapper.ToDTO(savedCourse);
    }

    public async Task<IReadOnlyList<CourseResponseDTO>> GetAllCoursesAsync(CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetAllAsync(cancellationToken);
        return courses.Select(CourseMapper.ToDTO).ToList();
    }

    // -----------------  Help by ChatGPT -------------------

    public async Task<CourseResponseDTO> UpdateCourseAsync(UpdateCourseDTO courseDTO, CancellationToken cancellationToken)
    {
        if(courseDTO.Name == null)
            throw new ArgumentNullException(nameof(courseDTO.Name));

        var courseToUpdate = await _courseRepository.GetByNameAsync(courseDTO.Name, cancellationToken);
        if (courseToUpdate != null)
            throw new ArgumentNullException(nameof(courseToUpdate));

        CourseMapper.UpdateEntity(courseToUpdate!, courseDTO);

        await _unitOfWork.CommitAsync(cancellationToken);

        return CourseMapper.ToDTO(courseToUpdate!);
    }
    // --------------------------------------------------------

    public async Task DeleteCourseAsync(string courseName, CancellationToken cancellationToken)
    {
        var courseToDelete = await _courseRepository.GetByNameAsync(courseName, cancellationToken);
        if (courseToDelete == null)
            throw new ArgumentNullException($"A course with the name {courseName} does not exist. Please try again.");

        await _courseRepository.DeleteAsync(courseToDelete, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }




    public async Task<LessonResponseDTO> CreateLessonToCourseAsync(Guid courseId, CreateLessonDTO lessonDTO, CancellationToken cancellationToken)
    {
        var savedLesson = LessonMapper.ToEntity(lessonDTO);
        await _lessonRepository.CreateAsync(savedLesson, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return LessonMapper.ToDTO(savedLesson);
    }

    public async Task DeleteLessonFromCourseAsync(Guid courseId, Guid lessonId, CancellationToken cancellationToken)
    {
        var lessonToDelete = await _lessonRepository.GetByIdAsync(lessonId,  cancellationToken);
        if (lessonToDelete == null)
            throw new ArgumentNullException($"A course with the name {lessonToDelete} does not exist. Please try again.");

        await _lessonRepository.DeleteAsync(lessonToDelete, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }




    public async Task<LocationResponseDTO> CreateLocationToCourseAsync(Guid courseId, CreateLocationDTO locationDTO, CancellationToken cancellationToken)
    {
        var savedLocation = LocationMapper.ToEntity(locationDTO);
        await _locationRepository.CreateAsync(savedLocation, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return LocationMapper.ToDTO(savedLocation);
    }

    public async Task DeleteLocationFromCourseAsync(Guid courseId, string locationName, CancellationToken cancellationToken)
    {
        var locationToDelete = await _locationRepository.GetByNameAsync(locationName, cancellationToken);
        if(locationToDelete == null)    
            throw new ArgumentNullException(nameof(locationToDelete));

        await _locationRepository.DeleteAsync(locationToDelete, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}