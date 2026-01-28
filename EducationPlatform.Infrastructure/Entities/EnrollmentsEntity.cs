using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.Domain.Entities;

public class EnrollmentsEntity
{
    [Key] // [Key] is not needed if the property are named Id. But i have choosen to use it anyway, for learning.
    public Guid Id { get; set; }
    public Guid ParticipantId { get; set; }
    public Guid LessonsId { get; set; }
    public byte[] Concurrency { get; set; } = null!;
    public DateTime EnrollmentDate { get; set; }

    public Guid StatusId { get; set; }
    public StatusEntity Status { get; set; } = null!;

}

