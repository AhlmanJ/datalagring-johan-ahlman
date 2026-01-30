namespace EducationPlatform.Domain.DTOs.Phonenumbers;

public sealed record PhonenumberResponseDTO
    (
        Guid Id,
        string PhoneNumber,
        Byte[] Concurrency
    );
