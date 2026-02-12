using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EducationPlatform.Domain.Entities;

public class PhonenumbersEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Phonenumber { get; set; } = null!;
    public byte[] Concurrency { get; set; } = null!;
    public Guid ParticipantId { get; set; }
    public virtual ParticipantsEntity Participant { get; set; } = null!;

    public PhonenumbersEntity() { }

    public PhonenumbersEntity(string phonenumber) 
    {
        ValidatePhoneNumber(phonenumber);

        this.Phonenumber = phonenumber;
    }

    public void ValidatePhoneNumber(string phonenumber)
    {
        if (string.IsNullOrWhiteSpace(phonenumber))
            throw new ArgumentException("Phonenumber is required, please try again..");

        var PhoneRegEx = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$";

        if (!Regex.IsMatch(phonenumber, PhoneRegEx))
            throw new ArgumentException("Invalid phonenumber, must be a valid phone number with or without + at the beginning.");
    }
}
