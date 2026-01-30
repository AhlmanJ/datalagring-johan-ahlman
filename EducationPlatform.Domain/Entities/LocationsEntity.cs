using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.Domain.Entities;

public class LocationsEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public byte[] Concurrency { get; set; } = null!;

    public virtual ICollection<LessonsEntity> Lessons { get; set; } = [];
}
