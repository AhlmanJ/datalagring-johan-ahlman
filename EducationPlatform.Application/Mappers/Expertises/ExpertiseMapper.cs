using EducationPlatform.Application.DTOs.Expertises;
using EducationPlatform.Application.Mappers.Instructors;
using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Application.Mappers.Expertises;

public static class ExpertiseMapper
{

    public static ExpertiseResponseDTO ToDTO(ExpertisesEntity entity)
        => new ExpertiseResponseDTO
        (
            Subject: entity.Subject ?? string.Empty
        );

    public static ExpertisesEntity ToEntity(CreateExpertiseDTO dto)
        => new ExpertisesEntity
        {
            Subject = dto.Subject
        };
}
