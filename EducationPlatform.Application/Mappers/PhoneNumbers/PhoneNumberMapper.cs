using EducationPlatform.Application.DTOs.Phonenumbers;
using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Application.Mappers.Phonenumbers;

public static class PhonenumberMapper
{
    public static PhonenumberResponseDTO ToDTO(PhonenumbersEntity entity)
        => new PhonenumberResponseDTO
        (
            ParticipantEmail: entity.Participant.Email,
            Phonenumber: entity.Phonenumber
        );

    public static PhonenumbersEntity ToEntity(CreatePhonenumberDTO dto)
    {
        var phonenumber = new PhonenumbersEntity
            (
                dto.Phonenumber.Trim().ToLower()
            );

        return phonenumber;
    }
}
