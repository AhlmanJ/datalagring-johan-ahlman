using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.Domain.Entities;

public class ExpertisesEntity
{
    [Key]
    public Guid Id { get; set; }
    public string? Subject { get; set; }
    public byte[] Concurrency { get; set; } = null!;


    public virtual ICollection<InstructorsEntity> Instructors { get; set; } = []; // Many-to-many relationship to InstructorEntity.
}
