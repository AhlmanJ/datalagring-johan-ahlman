
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
    private readonly IInstructorRepository _instructorRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CourseService(ICourseRepository courseRepository,ILocationRepository locationRepository, ILessonRepository lessonRepository, IInstructorRepository instructorRepository, IEnrollmentRepository enrollmentRepository, IUnitOfWork unitOfWork)
    {  _courseRepository = courseRepository;
       _locationRepository = locationRepository;
       _lessonRepository = lessonRepository;
       _instructorRepository = instructorRepository;
       _enrollmentRepository = enrollmentRepository;
       _unitOfWork = unitOfWork;
    }

    public async Task<CourseResponseDTO> CreateCourseAsync(CreateCourseDTO courseDTO, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(courseDTO.Name))
            throw new ArgumentException($"{courseDTO.Name} cannot be empty.");

        var checkCourse = await _courseRepository.ExistsAsync(c => c.Name == courseDTO.Name, cancellationToken);
        if (checkCourse)
            throw new ArgumentException($"A course with the name - {courseDTO.Name} - already exists!");

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
    public async Task<CourseResponseDTO> UpdateCourseAsync(Guid id, UpdateCourseDTO courseDTO, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullException(nameof(id));

        var courseToUpdate = await _courseRepository.GetByIdAsync(id, cancellationToken);
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

        var lessons = await _lessonRepository.GetAllAsync(cancellationToken);
        var courseWithLesson = lessons.Any(c => c.CourseId == id);

        if (courseWithLesson)
            throw new ArgumentException("You must delete all lessons related to the course before deleting a course!");

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

        if (lessonDTO.MaxCapacity < 1)
            throw new ArgumentException("Capacity cannot be less than 1. Please try again.");

        if (lessonDTO.StartDate > lessonDTO.EndDate)
            throw new ArgumentException("The lesson end date cannot be before the start date. Please try again.");

        var checkLesson = await _lessonRepository.ExistsAsync(l => l.Name == lessonDTO.Name, cancellationToken);
        if (checkLesson)
            throw new ArgumentException($"A lesson with the name - {lessonDTO.Name} - already exists!");

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

    public async Task<LessonResponseDTO> UpdateLessonAsync(Guid lessonId, UpdateLessonDTO updateLessonDTO, CancellationToken cancellationToken) 
    { 
        if(updateLessonDTO == null )
            throw new ArgumentNullException(nameof(updateLessonDTO));

        if (lessonId == Guid.Empty)
            throw new ArgumentNullException(nameof(lessonId));

        var lessonToUpdate = await _lessonRepository.GetByIdAsync(lessonId, cancellationToken);
        if (lessonToUpdate == null)
            throw new KeyNotFoundException(nameof(lessonToUpdate));

        LessonMapper.UpdateLesson(lessonToUpdate, updateLessonDTO);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        return LessonMapper.ToDTO(lessonToUpdate);
    }

    public async Task<bool> DeleteLessonFromCourseAsync(Guid lessonId, CancellationToken cancellationToken)
    {
        
        if (lessonId == Guid.Empty)
            throw new ArgumentNullException(nameof(lessonId));

        var lessonToDelete = await _lessonRepository.GetByIdAsync(lessonId,  cancellationToken);
        if (lessonToDelete == null)
            return false;

        var enrollments = await _enrollmentRepository.GetAllAsync(cancellationToken);
        var lessonsWithEnrollments = enrollments.Any(l => l.LessonsId == lessonId);

        if (lessonsWithEnrollments)
            throw new ArgumentException("You must delete all enrollments before deleting a lesson!");

        var location = lessonToDelete.Location; // Deletes the location for each lesson.

        await _lessonRepository.DeleteAsync(lessonToDelete, cancellationToken);

        await _locationRepository.DeleteAsync(location, cancellationToken);

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
            throw new ArgumentException("Location name cannot be empty. Please try again.");

        var locationToDelete = await _locationRepository.GetByNameAsync(locationName, cancellationToken);
        if (locationToDelete == null)
            return false;

        var lessons = await _lessonRepository.GetAllAsync(cancellationToken);
        var lessonWithLocation = lessons.Any(l => l.Name == locationName);

        if (lessonWithLocation)
            throw new ArgumentException("You must delete the lesson before deleting the location!");

        await _locationRepository.DeleteAsync(locationToDelete, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return true;
    }
}