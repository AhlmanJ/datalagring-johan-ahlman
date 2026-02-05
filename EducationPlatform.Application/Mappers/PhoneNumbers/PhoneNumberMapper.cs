using EducationPlatform.Application.DTOs.Phonenumbers;
using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Application.Mappers.Phonenumbers;

public static class PhoneNumberMapper
{
    public static PhonenumberResponseDTO ToDTO(PhonenumbersEntity entity)
        => new PhonenumberResponseDTO
        (
            Id: entity.Id,
            Phonenumber: entity.Phonenumber
        );

    public static PhonenumbersEntity ToDTO(CreatePhonenumberDTO dto)
        => new PhonenumbersEntity
        { 
            Phonenumber = dto.Phonenumber
        };
}
