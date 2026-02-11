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
            Name: entity.Name,
            StartDate: entity.StartDate,
            EndDate: entity.EndDate,
            MaxCapacity: entity.MaxCapacity,
            Enrolled: entity.NumberEnrolled,
            CourseName: entity.Course?.Name ?? "",
            Location: entity.Location.Name
        );

    public static LessonsEntity ToEntity(CreateLessonDTO dto)   
    {
        var lesson = new LessonsEntity
        (
            dto.Name,
            dto.StartDate,
            dto.EndDate,
            dto.MaxCapacity,
            dto.LocationName
        );

        lesson.Location = new LocationsEntity(dto.LocationName);

        return lesson;
    }

    public static UpdateLessonDTO UpdateLesson(LessonsEntity entity, UpdateLessonDTO dto)
    {
        if (dto.Name is not null)
        {
            entity.Name = dto.Name;
        }

        if (dto.StartDate != entity.StartDate)
        { 
            entity.StartDate = dto.StartDate;
        }

        if (dto.EndDate != entity.EndDate) 
        {
            entity.EndDate = dto.EndDate;
        }

        if (dto.MaxCapacity != entity.MaxCapacity)
        {
            entity.MaxCapacity = dto.MaxCapacity;
        }

        if (dto.CourseName is not null)
        {
            entity.Course.Name = dto.CourseName;
        }

        if (dto.Location!= entity.Location.Name)
        {
            entity.Location.Name = dto.Location;
        }

        return dto;
    }

}
