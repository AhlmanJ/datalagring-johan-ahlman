using EducationPlatform.Application.DTOs.Locations;
using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Application.Mappers.Locations;

public static class LocationMapper
{
    public static LocationResponseDTO ToDTO(LocationsEntity entity)
        => new LocationResponseDTO
        (
            Name: entity.Name
        );

    public static LocationsEntity ToEntity(CreateLocationDTO dto)
    {
        var locationName = new LocationsEntity
            (
                dto.Name.Trim().ToLower()
            );

        return locationName;
    }
}
