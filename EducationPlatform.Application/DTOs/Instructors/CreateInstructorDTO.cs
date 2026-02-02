using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.Application.DTOs.Instructors;

public sealed record CreateInstructorDTO
    (
        string FirstName,
        string LastName,

        // RegularExpression from ChatGPT.
        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email is invalid")]
        string Email
    );
