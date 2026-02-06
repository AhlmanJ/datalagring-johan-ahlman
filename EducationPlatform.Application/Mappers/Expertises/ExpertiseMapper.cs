using EducationPlatform.Application.DTOs.Expertises;
using EducationPlatform.Application.Mappers.Instructors;
using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Application.Mappers.Expertises;

public static class ExpertiseMapper
{

    public static ExpertiseResponseDTO ToDTO(ExpertisesEntity entity)
        => new ExpertiseResponseDTO
        (
            Id: entity.Id,
            Subject: entity.Subject!,
            Instructor: InstructorMapper.ToDTO(entity.Instructors.First())
        );

    public static ExpertisesEntity ToEntity(CreateExpertiseDTO dto)
        => new ExpertisesEntity
        {
            Subject = dto.Subject
        };
}
