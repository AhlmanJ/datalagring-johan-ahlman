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
    public string Expertise { get; set; } = string.Empty;
    public byte[] Concurrency { get; set; } = null!;
    public virtual ICollection<LessonsEntity> Lessons { get; set; } = [];


    public InstructorsEntity() { }

    public InstructorsEntity (string email, string firstname, string lastname, string expertise)
    {
        ValidateEmail(email);
        ValidateFirstName(firstname);
        ValidateLastName(lastname);
        ValidateExpertise(expertise);

        this.Email = email;
        this.FirstName = firstname;
        this.LastName = lastname;
        this.Expertise = expertise;
    }

    public void ValidateEmail(string email) 
    { 
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required, please try again..");

        var EmailRegEx = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

        if (!Regex.IsMatch(email, EmailRegEx))
            throw new ArgumentException("Invalid Email, use name@example.com");
    }

    public void ValidateFirstName(string firstname) 
    {
        if (string.IsNullOrWhiteSpace(firstname))
            throw new ArgumentException("First name is required");
    }

    public void ValidateLastName(string lastname)
    {
        if (string.IsNullOrWhiteSpace(lastname))
            throw new ArgumentException("Last name is required");
    }

    public void ValidateExpertise(string expertise)
    {
        if (string.IsNullOrWhiteSpace(expertise))
            throw new ArgumentException("Expertise is required");
    }
}
