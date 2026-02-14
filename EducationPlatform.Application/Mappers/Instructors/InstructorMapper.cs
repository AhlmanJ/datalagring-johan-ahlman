
// See Note!

using EducationPlatform.Application.DTOs.Instructors;
using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Application.Mappers.Instructors;

public static class InstructorMapper
{
    public static InstructorResponseDTO ToDTO(InstructorsEntity entity)
       => new InstructorResponseDTO
       (   
          InstructorId: entity.Id,
          FirstName: entity.FirstName,
          LastName: entity.LastName,
          Email: entity.Email,
          Expertise: entity.Expertise
       );

    public static AllInstructorsResponseDTO AllToDTO(InstructorsEntity entity)
        => new AllInstructorsResponseDTO
        (
          Email: entity.Email,
          FirstName: entity.FirstName,
          LastName: entity.LastName,
          Expertise: entity.Expertise
        );

    public static InstructorsEntity ToEntity(CreateInstructorDTO dto)
    {
        var instructor = new InstructorsEntity
            (
                dto.Email,
                dto.FirstName,
                dto.LastName,
                dto.Expertise
            );

        return instructor;
    }

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

        if(dto.Expertise is not null)
        { 
            entity.Expertise = dto.Expertise; 
        }

        entity.Concurrency = dto.Concurrency;
    }
}
