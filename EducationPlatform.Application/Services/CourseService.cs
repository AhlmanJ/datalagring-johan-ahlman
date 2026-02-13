
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
        if (string.IsNullOrWhiteSpace(courseDTO.Name))
            throw new ArgumentNullException($"{courseDTO.Name} cannot be empty.");

        var checkCourse = await _courseRepository.ExistsAsync(c => c.Name == courseDTO.Name, cancellationToken);
        if (checkCourse)
            throw new ArgumentException($"A course with the name {checkCourse} already exists!");

        var savedCourse = CourseMapper.ToEntity(courseDTO);
        await _courseRepository.CreateAsync(savedCourse, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return CourseMapper.ToDTO(savedCourse);
    }

    public async Task<IReadOnlyList<CourseResponseDTO>> GetAllCoursesAsync(CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetAllAsync(cancellationToken);
        if (courses == null)
            return new List<CourseResponseDTO>();

        return courses.Select(CourseMapper.ToDTO).ToList();
    }

    // -----------------  Help by ChatGPT -------------------
    public async Task<CourseResponseDTO> UpdateCourseAsync(string Name, UpdateCourseDTO courseDTO, CancellationToken cancellationToken)
    {
        if (courseDTO.Name == null)
            throw new ArgumentNullException(nameof(courseDTO.Name));

        var checkName = await _courseRepository.ExistsAsync(c => c.Name == Name, cancellationToken);
        if (!checkName)
            throw new KeyNotFoundException("Cannot find a Course with that name.");

        var courseToUpdate = await _courseRepository.GetByNameAsync(Name, cancellationToken);
        if (courseToUpdate == null)
            throw new KeyNotFoundException(nameof(courseToUpdate));

        CourseMapper.UpdateEntity(courseToUpdate!, courseDTO);

        await _unitOfWork.CommitAsync(cancellationToken);

        return CourseMapper.ToDTO(courseToUpdate!);
    }
    // --------------------------------------------------------
    public async Task<bool> DeleteCourseAsync(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullException(nameof(id));

        var courseToDelete = await _courseRepository.GetByIdAsync(id, cancellationToken);
        if (courseToDelete == null)
            return false;

        await _courseRepository.DeleteAsync(courseToDelete, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return true;
    }





    public async Task<LessonResponseDTO> CreateLessonToCourseAsync(Guid courseId, CreateLessonDTO lessonDTO, CancellationToken cancellationToken)
    {
        if (courseId == Guid.Empty)
            throw new ArgumentNullException(nameof(courseId));

        if (lessonDTO == null)
            throw new ArgumentNullException(nameof(lessonDTO));

        var savedLesson = LessonMapper.ToEntity(lessonDTO);
        savedLesson.CourseId = courseId;

        await _lessonRepository.CreateAsync(savedLesson, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return LessonMapper.ToDTO(savedLesson);
    }

    public async Task<IReadOnlyList<LessonResponseDTO>> GetAllLessonByCourseIdAsync(Guid courseId, CancellationToken cancellationToken)
    {
        if (courseId == Guid.Empty)
            throw new ArgumentNullException(nameof(courseId));

        var lessons = await _lessonRepository.GetAllByCourseIdAsync(courseId, cancellationToken);
        if (lessons.Count == 0)
            return new List<LessonResponseDTO>();

        return lessons.Select(LessonMapper.ToDTO).ToList();
    }

    public async Task<LessonResponseDTO> UpdateLessonAsync(string name, UpdateLessonDTO updateLessonDTO, CancellationToken cancellationToken) 
    { 
        if(updateLessonDTO == null )
            throw new ArgumentNullException(nameof(updateLessonDTO));

        if (name == null)
            throw new ArgumentException("Name cannot be empty");

        var lessonToUpdate = await _lessonRepository.GetByNameAsync(name, cancellationToken);
        if (lessonToUpdate == null)
            throw new KeyNotFoundException(nameof(lessonToUpdate));

        LessonMapper.UpdateLesson(lessonToUpdate, updateLessonDTO);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        return LessonMapper.ToDTO(lessonToUpdate);
    }

    public async Task<bool> DeleteLessonFromCourseAsync(Guid courseId, Guid lessonId, CancellationToken cancellationToken)
    {
        if (courseId == Guid.Empty)
            throw new ArgumentNullException(nameof(courseId));

        if (lessonId == Guid.Empty)
            throw new ArgumentNullException(nameof(lessonId));

        var lessonToDelete = await _lessonRepository.GetByIdAsync(lessonId,  cancellationToken);
        if (lessonToDelete == null)
            return false;
        
        

        await _lessonRepository.DeleteAsync(lessonToDelete, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return true;
    }





    public async Task<IReadOnlyList<LocationResponseDTO>> GetAllLocationsAsync(CancellationToken cancellationToken)
    {
        var locations = await _locationRepository.GetAllAsync(cancellationToken);
        if (locations.Count == 0)
            return new List<LocationResponseDTO>();

        return locations.Select(LocationMapper.ToDTO).ToList();
    }

    public async Task<bool> DeleteLocationFromCourseAsync(string locationName, CancellationToken cancellationToken)
    {
        if(locationName == null)
            throw new ArgumentNullException("Location name cannot be empty. Please try again.");

        var locationToDelete = await _locationRepository.GetByNameAsync(locationName, cancellationToken);
        if (locationToDelete == null)
            return false;

        await _locationRepository.DeleteAsync(locationToDelete, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return true;
    }
}