using EducationPlatform.Domain.DTOs.Phonenumbers;

namespace EducationPlatform.Domain.DTOs.Participants;

public sealed record CreateParticipantDTO
    (
        string FirstName,
        string LastName,
        string Email,
        List<CreatePhonenumberDTO>? Phonenumber
    );
