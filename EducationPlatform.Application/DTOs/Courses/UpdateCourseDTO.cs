/*
 * With this updateDTO I allow a user to partially update a course. 
 * I have also added a "concurrency guard" to prevent two users from updating a course at the same time.
 * This will instead throw a "concurrency exception" for the second update.
 */
namespace EducationPlatform.Application.DTOs.Courses;

public sealed record UpdateCourseDTO
    (
        Guid Id,
        byte[] Concurrency,
        string? Name = null!,
        string? Description = null!
    );
