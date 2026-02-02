using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.Domain.Entities;

public class StatusEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Status { get; set; } = null!;
    public byte[] Concurrency { get; set; } = null!;

    public virtual ICollection<EnrollmentsEntity> Enrollments { get; set; } = []; // Can have MANY enrollments.

}