using EducationPlatform.Application.DTOs.Participants;
using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Application.Mappers.Participants;

public static class ParticipantMapper
{
    public static ParticipantResponseDTO ToDTO(ParticipantsEntity entity)
        => new ParticipantResponseDTO
        (
            Id: entity.Id,
            FirstName: entity.FirstName,
            LastName: entity.LastName,
            Email: entity.Email,
            entity.Phonenumbers != null
            ? entity.Phonenumbers
            .Where(e => !string.IsNullOrEmpty(e.PhoneNumber))
            .Select(e => e.PhoneNumber!)
            .ToList() : null
        );

    public static ParticipantsEntity ToEntity(CreateParticipantDTO dto)
        => new ParticipantsEntity
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email
        };

    public static void UpdateEntity(ParticipantsEntity entity, UpdateParticipantDTO dto)
    {
        if (dto.FirstName is not null)
        {
            entity.FirstName = dto.FirstName;
        }

        if (dto.LastName is not null)
        {
            entity.LastName = dto.LastName;
        }

        if (dto.Email is not null)
        {
            entity.Email = dto.Email;
        }

        entity.Concurrency = dto.Concurrency;
    }
}
