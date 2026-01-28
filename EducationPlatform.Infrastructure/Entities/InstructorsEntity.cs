using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.Domain.Entities;

public class InstructorsEntity
{
    [Key] // [Key] is not needed if the property are named Id. But i have choosen to use it anyway, for learning.
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public byte[] Concurrency { get; set; } = null!;
    public virtual ICollection<LessonsEntity> Lessons { get; set; } = [];
    public virtual ICollection<ExpertisesEntity> Expertises { get; set; } = []; // Many-to-many relationship to ExpertiseEntity.
}
