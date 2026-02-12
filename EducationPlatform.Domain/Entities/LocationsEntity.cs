using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.Domain.Entities;

public class LocationsEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public byte[] Concurrency { get; set; } = null!;

    public virtual ICollection<LessonsEntity> Lessons { get; set; } = [];


    public LocationsEntity() { }

    public LocationsEntity(string name) 
    {
        ValidateName(name);

        this.Name = name;
    }

    public void ValidateName(string name) 
    { 
        if (string.IsNullOrEmpty(name)) 
            throw new ArgumentException("Name cannot be empty");
    }
}
