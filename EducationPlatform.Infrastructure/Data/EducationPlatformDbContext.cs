/*
 * I got help from chatGPT on how I could preset ready-made statuses for whether a participant is already booked for a lesson or not.
 * ChatGPT also helped me discover an error in the "InstructorsEntity".
 * The error was that I had copied a piece of code from my code that I had written for the "PracticipantsEntity" but forgot to change the name.
*/

using EducationPlatform.Application.Abstractions.Persistence;
using EducationPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationPlatform.Infrastructure.Data;

public sealed class EducationPlatformDbContext(DbContextOptions<EducationPlatformDbContext> options) : DbContext(options), IUnitOfWork
{


    // Defines the entities for the database.
    public DbSet<CoursesEntity> Courses => Set<CoursesEntity>();
    public DbSet<EnrollmentsEntity> Enrollments => Set<EnrollmentsEntity>();
    public DbSet<ExpertisesEntity> Expertises => Set<ExpertisesEntity>();
    public DbSet<InstructorsEntity> Instructors => Set<InstructorsEntity>();
    public DbSet<LessonsEntity> Lessons => Set<LessonsEntity>();
    public DbSet<LocationsEntity> Locations => Set<LocationsEntity>();
    public DbSet<ParticipantsEntity> Participants => Set<ParticipantsEntity>();
    public DbSet<PhonenumbersEntity> Phonenumbers => Set<PhonenumbersEntity>();
    public DbSet<StatusEntity> Status => Set<StatusEntity>();

    // UnitOfWork-implementation (By ChatGPT)
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Defines Primary Key, Data Types and more in all properties in the entities.

        modelBuilder.Entity<CoursesEntity>(entity =>
        {
            entity.ToTable(nameof(Courses));

            entity.HasKey(e => e.Id).HasName("PK_Courses_Id");
            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("(NEWSEQUENTIALID())", "DF_Courses_Id");

            entity.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

            entity.Property(e => e.Description)
            .HasMaxLength(300)
            .IsRequired();

            entity.Property(e => e.Concurrency)
            .IsRowVersion()
            .IsConcurrencyToken()
            .IsRequired();
        });

        modelBuilder.Entity<ParticipantsEntity>(entity =>
        {
            entity.ToTable(nameof(Participants));

            entity.HasKey(e => e.Id).HasName("PK_Participants_Id");
            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("(NEWSEQUENTIALID())", "DF_Participants_Id");

            entity.Property(e => e.FirstName)
            .HasMaxLength(50)
            .IsRequired();

            entity.Property(e => e.LastName)
            .HasMaxLength(50)
            .IsRequired();

            entity.Property(e => e.Email)
            .HasMaxLength(150)
            .IsUnicode(false) // Varchar instead of Nvarchar.
            .IsRequired();

            entity.Property(e => e.Concurrency)
            .IsRowVersion()
            .IsConcurrencyToken()
            .IsRequired();

            entity.HasIndex(e => e.Email, "UQ_Participants_Email").IsUnique();

            entity.ToTable(tb => tb.HasCheckConstraint("CK_Participants_Email_NotEmpty", "LTRIM(RTRIM([Email])) <> ''"));

        });

        modelBuilder.Entity<PhonenumbersEntity>(entity =>
        {
            entity.ToTable(nameof(Phonenumbers));

            entity.HasKey(e => e.Id).HasName("PK_Phonenumbers_Id");
            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("(NEWSEQUENTIALID())", "DF_Phonenumbers_Id");

            entity.Property(e => e.Phonenumber)
            .HasMaxLength(20)
            .IsUnicode(false);

            entity.Property(e => e.Concurrency)
            .IsRowVersion()
            .IsConcurrencyToken()
            .IsRequired();

            entity.HasOne(pn => pn.Participant)
            .WithMany(p => p.Phonenumbers)
            .HasForeignKey(pn => pn.ParticipantId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<EnrollmentsEntity>(entity =>
        {
            entity.ToTable(nameof(Enrollments));

            entity.HasKey(e => e.Id).HasName("PK_Enrollments_Id");
            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("(NEWSEQUENTIALID())", "DF_Enrollments_Id");

            entity.Property(e => e.EnrollmentDate)
            .HasPrecision(0)
            .ValueGeneratedOnAdd();

            entity.Property(e => e.Concurrency)
            .IsRowVersion()
            .IsConcurrencyToken()
            .IsRequired();

            // One-to-Many
            entity.HasOne<ParticipantsEntity>()
            .WithMany(p => p.Enrollments)
            .HasForeignKey(e => e.ParticipantId)
            .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<LessonsEntity>()
            .WithMany(l => l.Enrollments)
            .HasForeignKey(e => e.LessonsId)
            .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Status)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        });

        modelBuilder.Entity<ExpertisesEntity>(entity =>
        {
            entity.ToTable(nameof(Expertises));

            entity.HasKey(e => e.Id).HasName("PK_Expertises_Id");
            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("(NEWSEQUENTIALID())", "DF_Expertises_Id");

            entity.Property(e => e.Subject)
            .HasMaxLength(200);

            entity.Property(e => e.Concurrency)
            .IsRowVersion()
            .IsConcurrencyToken()
            .IsRequired();
        });

        modelBuilder.Entity<InstructorsEntity>(entity =>
        {
            entity.ToTable(nameof(Instructors));

            entity.HasKey(e => e.Id).HasName("PK_Instructors_Id");
            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("(NEWSEQUENTIALID())", "DF_Instructors_Id");

            entity.Property(e => e.Email)
            .HasMaxLength(150)
            .IsUnicode(false)
            .IsRequired();

            entity.Property(e => e.FirstName)
            .HasMaxLength(50)
            .IsRequired();

            entity.Property(e => e.LastName)
            .HasMaxLength(50)
            .IsRequired();

            entity.Property(e => e.Concurrency)
            .IsRowVersion()
            .IsConcurrencyToken()
            .IsRequired();


            // Changed in later branch: I had copied the code snippet from "ParticipantsEntity" but forgot to rename them to "Instructor".
            entity.HasIndex(e => e.Email, "UQ_InstructorsEntity_Email").IsUnique();

            entity.ToTable(tb => tb.HasCheckConstraint("CK_Instructors_Email_NotEmpty", "LTRIM(RTRIM([Email])) <> ''"));
        });

        modelBuilder.Entity<InstructorsEntity>() // Use simple "join" as i don't have extra data.
            .HasMany(i => i.Expertises)
            .WithMany(e => e.Instructors)
            .UsingEntity(j => j.ToTable("InstructorExpertises"));

        modelBuilder.Entity<LessonsEntity>(entity =>
        {
            entity.ToTable(nameof(Lessons));

            entity.HasKey(e => e.Id).HasName("PK_Lessons_Id");
            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("(NEWSEQUENTIALID())", "DF_Lessons_Id");

            entity.Property(e => e.Name)
            .HasMaxLength(200)
            .IsRequired();

            entity.Property(e => e.StartDate)
            .HasPrecision(0)
            .ValueGeneratedOnAdd();

            entity.Property(e => e.EndDate)
            .HasPrecision(0)
            .ValueGeneratedOnAdd();

            entity.Property(e => e.Concurrency)
            .IsRowVersion()
            .IsConcurrencyToken()
            .IsRequired();
        });

        modelBuilder.Entity<LessonsEntity>()
            .HasMany(l => l.Instructors)
            .WithMany(i => i.Lessons)
            .UsingEntity(j => j.ToTable("LessonInstructors"));

        modelBuilder.Entity<LessonsEntity>()
            .HasOne(l => l.Course)
            .WithMany(c => c.Lessons)
            .HasForeignKey(l => l.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<LessonsEntity>()
            .HasOne(l => l.Location)
            .WithMany(l => l.Lessons)
            .HasForeignKey(l => l.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<LocationsEntity>(entity =>
        {
            entity.ToTable(nameof(Locations));

            entity.HasKey(e => e.Id).HasName("PK_Locations_Id");
            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("(NEWSEQUENTIALID())", "DF_Locations_Id");

            entity.Property(e => e.Name)
            .HasMaxLength(200)
            .IsRequired();

            entity.Property(e => e.Concurrency)
            .IsRowVersion()
            .IsConcurrencyToken()
            .IsRequired();
        });

        modelBuilder.Entity<StatusEntity>(entity =>
        {
            entity.ToTable(nameof(Status));

            entity.HasKey(e => e.Id).HasName("PK_Status_Id");
            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("(NEWSEQUENTIALID())", "DF_Status_Id");

            entity.Property(e => e.Status)
            .HasMaxLength(20)
            .IsRequired();

            entity.Property(e => e.Concurrency)
            .IsRowVersion()
            .IsConcurrencyToken()
            .IsRequired();

            // Help by chatGPT.

            entity.HasData(
                new StatusEntity { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Status = "Unbooked" },
                new StatusEntity { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Status = "Booked" }
            );
        });
    }
}
