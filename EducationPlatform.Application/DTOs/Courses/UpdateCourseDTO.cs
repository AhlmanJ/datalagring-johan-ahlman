/*
 * With this updateDTO I allow a user to partially update a course. 
 */
namespace EducationPlatform.Application.DTOs.Courses;

public sealed record UpdateCourseDTO
    (
        string? Name = null!,
        string? Description = null!
    );
