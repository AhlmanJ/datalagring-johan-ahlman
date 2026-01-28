using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.Domain.Entities;

public class ParticipantsEntity
{
    [Key] // [Key] is not needed if the property are named Id. But i have choosen to use it anyway, for learning.
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public byte[] Concurrency { get; set; } = null!;

    // Specifies a relationship to the other entity to be able to use something called lazy loading at a later time.
    // This is not something you have to use, but it can be useful if you want to use Join statements.
    public virtual ICollection<PhonenumbersEntity> Phonenumbers { get; set; } = new List<PhonenumbersEntity>();

    public virtual ICollection<EnrollmentsEntity> Enrollments { get; set; } = [];
}

