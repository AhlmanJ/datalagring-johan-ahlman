
// See Note!

using EducationPlatform.Application.DTOs.Instructors;
using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Application.Mappers.Instructors;

public static class InstructorMapper
{
    public static InstructorResponseDTO ToDTO(InstructorsEntity entity)
       => new InstructorResponseDTO
       (
          Id: entity.Id,
          FirstName: entity.FirstName!,
          LastName: entity.LastName!,
          Email: entity.Email!,
          Subject: entity.Expertises != null && entity.Expertises.Any() // With support from chatGPT as I mentioned in ParticipantMapper.
            ? entity.Expertises
            .Where(e => !string.IsNullOrEmpty(e.Subject))
            .Select(e => e.Subject!)
            .ToList() : null
       );

    public static InstructorsEntity ToEntity(CreateInstructorDTO dto)
    {
        var instructor = new InstructorsEntity
            (
                dto.FirstName,
                dto.LastName,
                dto.Email
            );

        return instructor;
    }

    public static void UpdateEntity(InstructorsEntity entity, UpdateInstructorDTO dto)
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
