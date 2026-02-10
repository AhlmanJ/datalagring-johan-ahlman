
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
using EducationPlatform.Domain.Middlewares;
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

    public async Task<CourseResponseDTO> CreateCourseAsync(CreateCourseDTO courseDTO, string Name, CancellationToken cancellationToken)
    {
        if(string.IsNullOrWhiteSpace(Name))
            throw new ArgumentNullException($"{Name} cannot be empty.");

        var checkCourse = await _courseRepository.ExistsAsync(c => c.Name == Name, cancellationToken);
        if(checkCourse)
            throw new ArgumentException($"A course with the name {checkCourse} already exists!");

        var savedCourse = CourseMapper.ToEntity(courseDTO);
        await _courseRepository.CreateAsync(savedCourse, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return CourseMapper.ToDTO(savedCourse);
    }

    public async Task<IReadOnlyList<CourseResponseDTO>> GetAllCoursesAsync(CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetAllAsync(cancellationToken);

        if (courses.Count == 0)
            throw new DomainException("No Courses available");

        return courses.Select(CourseMapper.ToDTO).ToList();
    }

    // -----------------  Help by ChatGPT -------------------

    public async Task<CourseResponseDTO> UpdateCourseAsync(string Name, UpdateCourseDTO courseDTO, CancellationToken cancellationToken)
    {
        if (courseDTO.Name == null)
            throw new ArgumentNullException(nameof(courseDTO.Name));

        var checkName = await _courseRepository.ExistsAsync(c => c.Name == Name, cancellationToken);
        if (!checkName)
            throw new ArgumentException("Cannot find a Course with that name.");

        var courseToUpdate = await _courseRepository.GetByNameAsync(Name, cancellationToken);
        if (courseToUpdate == null)
            throw new ArgumentNullException(nameof(courseToUpdate));

        CourseMapper.UpdateEntity(courseToUpdate!, courseDTO);

        await _unitOfWork.CommitAsync(cancellationToken);

        return CourseMapper.ToDTO(courseToUpdate!);
    }
    // --------------------------------------------------------

    public async Task<bool> DeleteCourseAsync(Guid id, CancellationToken cancellationToken)
    {
        var courseToDelete = await _courseRepository.GetByIdAsync(id, cancellationToken);
        if (courseToDelete == null)
            return false;

        await _courseRepository.DeleteAsync(courseToDelete, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return true;
    }




    public async Task<LessonResponseDTO> CreateLessonToCourseAsync(Guid courseId, CreateLessonDTO lessonDTO, CancellationToken cancellationToken)
    {
 
        var savedLesson = LessonMapper.ToEntity(lessonDTO);
        savedLesson.CourseId = courseId;
        await _lessonRepository.CreateAsync(savedLesson, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return LessonMapper.ToDTO(savedLesson);
    }

    public async Task<bool> DeleteLessonFromCourseAsync(Guid courseId, Guid lessonId, CancellationToken cancellationToken)
    {
        var lessonToDelete = await _lessonRepository.GetByIdAsync(lessonId,  cancellationToken);
        if (lessonToDelete == null)
            return false;

        await _lessonRepository.DeleteAsync(lessonToDelete, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return true;
    }


    // -----------------  Help by ChatGPT -------------------

    public async Task<LocationResponseDTO> CreateLocationToCourseAsync(Guid lessonId, string lessonName, CreateLocationDTO locationDTO, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetByNameAsync(lessonName, cancellationToken);
        if (lesson == null)
            throw new ArgumentNullException($"Could not find a lesson with the name {lesson}");
       
        var savedLocation = LocationMapper.ToEntity(locationDTO);
        await _locationRepository.CreateAsync(savedLocation, cancellationToken);

        lesson.LocationId = savedLocation.Id;
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return LocationMapper.ToDTO(savedLocation);
    }
    // --------------------------------------------------------

    public async Task<bool> DeleteLocationFromCourseAsync(Guid courseId, string locationName, CancellationToken cancellationToken)
    {
        var locationToDelete = await _locationRepository.GetByNameAsync(locationName, cancellationToken);
        if (locationToDelete == null)
            return false;

        await _locationRepository.DeleteAsync(locationToDelete, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return true;
    }
}