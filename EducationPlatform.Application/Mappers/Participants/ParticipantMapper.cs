
// Help from ChatGPT - See notifications.

using EducationPlatform.Application.DTOs.Participants;
using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Application.Mappers.Participants;

public static class ParticipantMapper
{
    public static ParticipantResponseDTO ToDTO(ParticipantsEntity entity)
        => new ParticipantResponseDTO
        (
            ParticipantId: entity.Id, 
            FirstName: entity.FirstName,
            LastName: entity.LastName,
            Email: entity.Email,
            Phonenumber: entity.Phonenumbers != null // When I tested the endpoint, if I added a phone number to the participant, it showed as registration status. This was because I had not specified "Phonenumber:" as the "identifier".
            ? entity.Phonenumbers
            .Where(e => !string.IsNullOrEmpty(e.Phonenumber))
            .Select(e => e.Phonenumber!)
            .ToList() : null
        );


    //Here I got help from ChatGPT on how to enable a user to add phone numbers to a Participant.
    public static ParticipantsEntity ToEntity(CreateParticipantDTO dto)
    {
        var participant = new ParticipantsEntity
            (
                dto.FirstName.Trim().ToLower(),
                dto.LastName.Trim().ToLower(),
                dto.Email.Trim().ToLower()
            );

        participant.Phonenumbers = dto.Phonenumber != null // Checks if the DTO has a phone number or if it is Null.
                ? dto.Phonenumber!
                .Where(Phonenumbers => !string.IsNullOrEmpty(Phonenumbers))
                .Select(Phonenumbers => new PhonenumbersEntity { Phonenumber = Phonenumbers.Trim().ToLower() }) // for each string in the list, a new entity PhonenumbersEntity is created
                .ToList() : new List<PhonenumbersEntity>(); // Convert what we get from .Select into a List

        return participant;
    }

    public static void UpdateEntity(ParticipantsEntity entity, UpdateParticipantDTO dto)
    {
        if (dto.FirstName is not null)
        {
            entity.FirstName = dto.FirstName.Trim().ToLower();
        }

        if (dto.LastName is not null)
        {
            entity.LastName = dto.LastName.Trim().ToLower();
        }
    }
}
