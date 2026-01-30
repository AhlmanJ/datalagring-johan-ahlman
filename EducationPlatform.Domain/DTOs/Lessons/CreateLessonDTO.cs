namespace EducationPlatform.Domain.DTOs.Lessons;

public sealed record CreateLessonDTO
    (
        Guid CourseId, // FK
        string Name,
        DateTime StartDate, 
        DateTime EndDate, 
        int MaxCapacity, 
        Guid LocationId, // FK
        List<Guid> InstructorIds // FK , Only exists in the DTO so that the front end knows which instructors to associate with a particular lesson.
    );
