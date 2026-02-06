
// Help from ChatGPT - See notifications.

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
            .Where(e => !string.IsNullOrEmpty(e.Phonenumber))
            .Select(e => e.Phonenumber!)
            .ToList() : null,

            entity.Enrollments != null
            ? entity.Enrollments
            .Select(e => e.Status.Status!)
            .ToList() : null
        );

    //Here I got help from ChatGPT on how to enable a user to add phone numbers to a Participant.
    public static ParticipantsEntity ToEntity(CreateParticipantDTO dto)
    {
        var participant = new ParticipantsEntity
            (
                dto.FirstName,
                dto.LastName,
                dto.Email
            );

        participant.Phonenumbers = dto.Phonenumber != null // Checks if the DTO has a phone number or if it is Null.
                ? dto.Phonenumber!
                .Where(Phonenumbers => !string.IsNullOrEmpty(Phonenumbers))
                .Select(Phonenumbers => new PhonenumbersEntity { Phonenumber = Phonenumbers }) // for each string in the list, a new entity PhonenumbersEntity is created
                .ToList() : new List<PhonenumbersEntity>(); // Convert what we get from .Select into a List

        return participant;
    }

    public static void UpdateEntity(ParticipantsEntity entity, UpdateParticipantDTO dto)
    {
        entity.Id = dto.Id;

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
