using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.Domain.Entities;

public class PhonenumbersEntity
{
    [Key]
    public Guid Id { get; set; }
    public string? PhoneNumber { get; set; }
    public byte[] Concurrency { get; set; } = null!;
    public Guid ParticipantId { get; set; }
    public virtual ParticipantsEntity Participant { get; set; } = null!;
}
