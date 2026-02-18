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
            entity.Id,
            Name: entity.Name,
            StartDate: entity.StartDate,
            EndDate: entity.EndDate,
            MaxCapacity: entity.MaxCapacity,
            Enrolled: entity.NumberEnrolled,
            Location: entity.Location.Name,
            Instructors: entity.Instructors != null ? entity.Instructors.Where(i => !string.IsNullOrEmpty(i.Email)).Select(i => i.Email!).ToList(): null
        );

    public static LessonsEntity ToEntity(CreateLessonDTO dto)   
    {
        var lesson = new LessonsEntity
        (
            dto.Name.Trim().ToLower(),
            dto.StartDate,
            dto.EndDate,
            dto.MaxCapacity,
            dto.LocationName.Trim().ToLower()
        );

        lesson.Location = new LocationsEntity(dto.LocationName.Trim().ToLower());

        return lesson;
    }

    public static UpdateLessonDTO UpdateLesson(LessonsEntity entity, UpdateLessonDTO dto)
    {
        if (dto.Name is not null)
        {
            entity.Name = dto.Name.Trim().ToLower();
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

        return dto;
    }

}
