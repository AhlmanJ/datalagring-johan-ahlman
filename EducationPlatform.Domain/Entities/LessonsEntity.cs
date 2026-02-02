using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.Domain.Entities;

public class LessonsEntity
{
    [Key] // [Key] is not needed if the property are named Id. But i have choosen to use it anyway, for learning.
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; }
    public int MaxCapacity { get; set; }

    public Guid CourseId { get; set; } // FK
    public virtual CoursesEntity Course { get; set; } = null!; // Navigation property

    public byte[] Concurrency { get; set; } = null!;

    public Guid LocationId { get; set; } // FK
    public virtual LocationsEntity Location { get; set; } = null!; // Navigation property

    public virtual ICollection<EnrollmentsEntity> Enrollments { get; set; } = [];

    public virtual ICollection<InstructorsEntity> Instructors { get; set; } = [];
}
