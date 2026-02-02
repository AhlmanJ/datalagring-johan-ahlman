using EducationPlatform.Application.DTOs.Locations;
using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Application.Mappers.Locations;

public static class LocationMapper
{
    public static LocationResponseDTO ToDTO(LocationsEntity entity)
        => new LocationResponseDTO
        (
            Id: entity.Id,
            Name: entity.Name
        );

    public static LocationsEntity ToEntity(CreateLocationDTO dto)
            => new LocationsEntity
            {
                Name = dto.Name
            };

    public static void UpdateEntity(LocationsEntity entity, UpdateLocationDTO dto)
    {
        if (dto.Name is not null)
        {
            entity.Name = dto.Name;
        }

        entity.Concurrency = dto.Concurrency;
    }
}
