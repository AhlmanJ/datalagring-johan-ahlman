using EducationPlatform.Application.DTOs.PhoneNumbers;
using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Application.Mappers.Phonenumbers;

public static class PhoneNumberMapper
{
    public static PhoneNumberResponseDTO ToDTO(PhonenumbersEntity entity)
        => new PhoneNumberResponseDTO
        (
            Id: entity.Id,
            PhoneNumber: entity.PhoneNumber
        );

    public static PhonenumbersEntity ToDTO(CreatePhoneNumberDTO dto)
        => new PhonenumbersEntity
        { 
            PhoneNumber = dto.PhoneNumber
        };
}
