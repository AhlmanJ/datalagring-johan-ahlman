using EducationPlatform.Domain.Middlewares;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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


    public InstructorsEntity() { }

    public InstructorsEntity (string email, string firstname, string lastname)
    {
        ValidateEmail(email);
        ValidateFirstName(firstname);
        ValidateLastName(lastname);

        this.Email = email;
        this.FirstName = firstname;
        this.LastName = lastname;
    }

    public void ValidateEmail(string email) 
    { 
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email is required, please try again..");

        var EmailRegEx = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

        if (Regex.IsMatch(email, EmailRegEx))
            throw new DomainException("Invalid Email, use name@example.com");
    }

    public void ValidateFirstName(string firstname) 
    {
        if (string.IsNullOrWhiteSpace(firstname))
            throw new DomainException("First name is required");
    }

    public void ValidateLastName(string lastname)
    {
        if (string.IsNullOrWhiteSpace(lastname))
            throw new DomainException("Last name is required");
    }
}
