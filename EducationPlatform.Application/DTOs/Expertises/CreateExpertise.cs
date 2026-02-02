namespace EducationPlatform.Application.DTOs.Expertises;

public sealed record CreateExpertiseDTO
    (
        string Subject,
        List<Guid> InstructorId
    );