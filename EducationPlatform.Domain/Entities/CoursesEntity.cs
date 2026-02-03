
/* 
 * I consulted ChatGPT about how to validate a domain layer. But I also found some information about this on the internet.
 * I also got help from ChatGPT on how to create "Description" as an optional parameter.
*/

using EducationPlatform.Domain.Middlewares;
using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.Domain.Entities;

public class CoursesEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public byte[] Concurrency { get; set; } = null!;

    public virtual ICollection<LessonsEntity> Lessons { get; set; } = [];

    // Parameterless constructor for EF Core.
    public CoursesEntity(){} 

    public CoursesEntity(string name)
    {
        ValidateName(name);
    }

    public void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name cannot be empty");

        Name = name.Trim();
    }

    // Help from ChatGPT on how to create "Description" as an optional parameter. (Code from ChatGPT)!
    public void ValidateDescription(string description)
    {
        Description = description?.Trim() ?? string.Empty;
    }
}
