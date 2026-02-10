using EducationPlatform.Domain.Middlewares;
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


    public LessonsEntity() { }

    public LessonsEntity(string name, DateTime startdate, DateTime enddate, int maxcapacity, string lessonname)
    {
        ValidateName(name);
        ValidateDate(startdate, enddate);
        ValidateCapacity(maxcapacity);

        this.Name = name;
        this.StartDate = startdate;
        this.EndDate = enddate;
        this.MaxCapacity = maxcapacity;
    }

    public void ValidateName(string name) 
    { 
        if (string.IsNullOrEmpty(name))
            throw new DomainException("name");
    }

    public void ValidateDate(DateTime startdate, DateTime enddate)
    {
        if (startdate > enddate)
            throw new DomainException("The Start date cannot be after End date");
    }

    public void ValidateCapacity(int maxcapacity)
    {
        if (maxcapacity < 1)
            throw new DomainException("Maxcapacity must be grater than 0");
    }
}
