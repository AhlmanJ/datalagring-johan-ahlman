using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.Domain.Entities;

public class CoursesEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public byte[] Concurrency { get; set; } = null!;

    public virtual ICollection<LessonsEntity> Lessons { get; set; } = [];
}
