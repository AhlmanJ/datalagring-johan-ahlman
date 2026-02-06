/*
 * I have received help from chatGPT on how to map Location and Instructors in this file but I have also had it explained to me why I should do it this way.
 * My idea is that a user should be able to create a lesson without being forced to add a location and an instructor directly when the lesson is created.
 * This should be optional.
 */

using EducationPlatform.Application.DTOs.Instructors;
using EducationPlatform.Application.DTOs.Lessons;
using EducationPlatform.Application.DTOs.Locations;
using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Application.Mappers.Lessons;

public static class LessonMapper
{

    public static LessonResponseDTO ToDTO(LessonsEntity entity)
        => new LessonResponseDTO
        (
            Id: entity.Id,
            Name: entity.Name,
            StartDate: entity.StartDate,
            EndDate: entity.EndDate,
            CourseId: entity.CourseId,
            CourseName: entity.Course.Name,
            MaxCapacity: entity.MaxCapacity,

            /*
             * Maps LessonsEntity.Location to LocationResponseDTO and checks if a Location has been filled in when creating a lesson.
             * If the user has filled in a location for the lesson event, they are inserted into Location. If the user has left these fields empty, a Null value is set.
             */
            Location: entity.Location != null
                ? new LocationResponseDTO(entity.Location.Id, entity.Location.Name)
                : null!,

            /*
             * Maps each instructor to a list, but if this field is left blank by a user, an empty list is created instead.
             */
            Instructors: entity.Instructors
                .Select(i => new InstructorResponseDTO
                    (
                        Id: i.Id,
                        FirstName: i.FirstName,
                        LastName: i.LastName,
                        Email: i.Email
                    ))
                .ToList() ?? new List<InstructorResponseDTO>()
        );

    public static LessonsEntity ToEntity(CreateLessonDTO dto)   
    {
        var lesson = new LessonsEntity
        (
            dto.Name,
            dto.StartDate,
            dto.EndDate,
            dto.MaxCapacity
        );

        return lesson;
    }

}
