using EducationPlatform.Domain.Interfaces;
using EducationPlatform.Domain.Repositories;
using EducationPlatform.Infrastructure.Data;
using EducationPlatform.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Registering DbContext.
builder.Services.AddDbContext<EducationPlatformDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("EducationPlatformDB"),
    // "Tells" where the migration files should be saved.
    sql => sql.MigrationsAssembly("EducationPlatform.Infrastructure")
    ));

// Repositories
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IExpertiseRepository, ExpertiseRepository>();
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<IPhonenumberRepository, PhonenumberRepository>();

var app = builder.Build();
 
app.MapOpenApi();
app.UseHttpsRedirection();

app.Run();
