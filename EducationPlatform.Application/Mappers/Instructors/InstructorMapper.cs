using EducationPlatform.Application.DTOs.Instructors;
using EducationPlatform.Application.Mappers.Expertises;
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
          Email: entity.Email!
       );

    public static InstructorsEntity ToEntity(CreateInstructorDTO dto)
        => new InstructorsEntity
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
        };

    public static void UpdateEntity(InstructorsEntity entity, UpdateInstructorDTO dto)
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
