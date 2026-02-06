
// Here chatGPT has helped me implement Unit Of Work. See note in the code.

using EducationPlatform.Application.Abstractions.Persistence;
using EducationPlatform.Application.ServiceInterfaces;
using EducationPlatform.Application.Services;
using EducationPlatform.Domain.Interfaces;
using EducationPlatform.Domain.Repositories;
using EducationPlatform.Infrastructure.Data;
using EducationPlatform.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCors();

// Registering DbContext.
builder.Services.AddDbContext<EducationPlatformDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("EducationPlatformDB"),
    // "Tells" where the migration files should be saved.
    sql => sql.MigrationsAssembly("EducationPlatform.Infrastructure")
    ));

// ----------- Code by ChatGPT -------------

builder.Services.AddScoped<IUnitOfWork>(provider =>
    provider.GetRequiredService<EducationPlatformDbContext>());

// -----------------------------------------

// Services:
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();

// Repositories:
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
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); // To be able to use for example a REACT frontend.

// EndPoints:

// CREATE

// READ

// UPDATE

// DELETE

app.Run();
