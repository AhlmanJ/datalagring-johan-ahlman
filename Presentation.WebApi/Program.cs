// Here chatGPT has helped me implement Unit Of Work. See note in the code.

using EducationPlatform.Application.Abstractions.Persistence;
using EducationPlatform.Application.DTOs.Courses;
using EducationPlatform.Application.DTOs.Enrollments;
using EducationPlatform.Application.DTOs.Expertises;
using EducationPlatform.Application.DTOs.Instructors;
using EducationPlatform.Application.DTOs.Lessons;
using EducationPlatform.Application.DTOs.Locations;
using EducationPlatform.Application.DTOs.Participants;
using EducationPlatform.Application.DTOs.Phonenumbers;
using EducationPlatform.Application.Helpers;
using EducationPlatform.Application.ServiceInterfaces;
using EducationPlatform.Application.Services;
using EducationPlatform.Domain.Interfaces;
using EducationPlatform.Domain.Repositories;
using EducationPlatform.Infrastructure.Data;
using EducationPlatform.Infrastructure.Repositories;
using EducationPlatform.Presentation.Api.Middleware;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

// From the in school lesson. (Example-code.) 
// For GlobalExceptionHandler:
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        // Adding a unique Id for the specific HTTP-Request that could be usefull while debugging.
        context.ProblemDetails.Extensions["requestId"] = context.HttpContext.TraceIdentifier;
    };
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

// For Swagger:
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapOpenApi();
app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); // To be able to use for example a REACT frontend.

app.UseExceptionHandler();

// EndPoints:

#region Courses
var courses = app.MapGroup("/api/courses").WithTags("Courses");

// CREATE
courses.MapPost("/api/courses", async (CreateCourseDTO dto, string Name,ICourseService courseService, CancellationToken ct) =>
{
    var result = await courseService.CreateCourseAsync(dto, Name, ct);

    return Results.Created("/", result);
});

courses.MapPost("/api/coursesCreateLesson", async (Guid courseId, CreateLessonDTO dto, ICourseService courseService, CancellationToken ct) =>
{
    var result = await courseService.CreateLessonToCourseAsync(courseId, dto, ct);

    return Results.Created("/", result);
});

// READ
courses.MapGet("/", async (ICourseService courseService, CancellationToken ct) => 
{ 
    var result = await courseService.GetAllCoursesAsync(ct);

    return Results.Ok(result);
});

courses.MapGet("/GetAllLessons{courseId}", async (Guid courseId, ICourseService courseService, CancellationToken ct) =>
{
    var result = await courseService.GetAllLessonByCourseIdAsync(courseId, ct);

    return Results.Ok(result);
});

courses.MapGet("/GetAllLocations", async (ICourseService courseService, CancellationToken ct) =>
{ 
    var result = await courseService.GetAllLocationsAsync(ct);

    return Results.Ok(result);
});


// UPDATE
courses.MapPut("/{Name}", async (string Name, UpdateCourseDTO dto, ICourseService courseService, CancellationToken ct) =>
{
    var result = await courseService.UpdateCourseAsync(Name, dto, ct);
    return Results.Ok(result);
});

courses.MapPut("/UpdateLesson/{Name}", async(string Name, UpdateLessonDTO updateLessonDTO, ICourseService courseService, CancellationToken ct) =>
{ 
    var result = await courseService.UpdateLessonAsync(Name, updateLessonDTO, ct);

    return Results.Ok(result);
});

// DELETE
courses.MapDelete("/{id:guid}", async (Guid id, ICourseService courseService, CancellationToken ct) =>
{
    await courseService.DeleteCourseAsync(id, ct);
});

courses.MapDelete("/{courseId}/lessons/{lessonId}", async (Guid courseId, Guid lessonId, ICourseService courseService, CancellationToken ct) =>
{
    var result = await courseService.DeleteLessonFromCourseAsync(courseId, lessonId, ct);

     return result ? Results.NoContent() : Results.BadRequest();
});

courses.MapDelete("/locations/{locationName}", async (string locationName, ICourseService courseService, CancellationToken ct) =>
{
    var result = await courseService.DeleteLocationFromCourseAsync(locationName, ct);

    return result ? Results.NoContent() : Results.BadRequest();
});
#endregion



#region Enrollments
var enrollments = app.MapGroup("/api/enrollments").WithTags("Enrollments");

// CREATE
enrollments.MapPost("/api/enrollments", async (CreateEnrollmentDTO dto, Guid participantId, Guid lessonsId, IEnrollmentService enrollmentService, CancellationToken ct) =>
{
    var result = await enrollmentService.CreateEnrollmentAsync(dto, participantId, lessonsId, ct);

    return Results.Created("/", result);
});

// READ
enrollments.MapGet("/getEnrollments", async (IEnrollmentService enrollmentService, CancellationToken ct) =>
{
    var result = await enrollmentService.GetAllEnrollmentsAsync(ct);

    return Results.Ok(result);
});

// DELETE
enrollments.MapDelete("/{Id:guid}/{lessonName}/{participantId}", async (Guid enrollmentId, string lessonName, Guid participantId, IEnrollmentService enrollmentService, CancellationToken ct) =>
{
    var result = await enrollmentService.DeleteEnrollmentAsync(enrollmentId, lessonName, participantId, ct);

    return result ? Results.NoContent() : Results.BadRequest();
});

#endregion



#region Instructors
var instructors = app.MapGroup("/api/instructors").WithTags("Instructors");

// CREATE
instructors.MapPost("/api/instructors", async (CreateInstructorDTO dto, IInstructorService instructorService, CancellationToken ct) =>
{
    var result = await instructorService.CreateInstructorAsync(dto, ct);

    return Results.Created("/", result);
});

instructors.MapPost("/api/instructorsCreateExpertise", async (Guid instructorId, CreateExpertiseDTO dto, IInstructorService instructorService, CancellationToken ct) =>
{
    var result = await instructorService.CreateExpertiseToInstructorAsync(instructorId, dto, ct);

    return Results.Created("/", result);
});

// READ
instructors.MapGet("/getInstructors", async (IInstructorService instructorService, CancellationToken ct) =>
{
    var result = await instructorService.GetAllInstructorsAsync(ct);

    return Results.Ok(result);
});

instructors.MapGet("/getInstructor/{email}", async (string email, IInstructorService instructorService, CancellationToken ct) =>
{ 
    var result = await instructorService.GetInstructorByEmailAsync(email, ct);

    return Results.Ok(result);
});

// UPDATE
instructors.MapPut("/instructors/{Id:guid}", async (Guid id, UpdateInstructorDTO dto, IInstructorService instructorService, CancellationToken ct) =>
{
    var result = await instructorService.UpdateInstructorAsync(id, dto, ct);
    return Results.Ok(result);
});

// DELETE
instructors.MapDelete("/instructors/{Id:guid}", async (Guid id, IInstructorService instructorService, CancellationToken ct) =>
{
    var result = await instructorService.DeleteInstructorAsync(id, ct);

    return result ? Results.NoContent() : Results.BadRequest();
});

instructors.MapDelete("/{instructorId}/expertise/{Id:guid}", async (Guid instructorId, Guid id, IInstructorService instructorService, CancellationToken ct) =>
{
    var result = await instructorService.DeleteExpertiseFromInstructorAsync(instructorId, id, ct);

    return result ? Results.NoContent() : Results.BadRequest();
});

#endregion



#region Participants
var participants = app.MapGroup("/api/participants").WithTags("Participants");

// CREATE
participants.MapPost("/api/participants", async (CreateParticipantDTO dto, IParticipantService participantService, CancellationToken ct) =>
{
    var result = await participantService.CreateParticipantAsync(dto, ct);

    return Results.Created("/", result);
});

participants.MapPost("/api/participantsCreatePhonenumber", async (Guid participantId, CreatePhonenumberDTO dto, IParticipantService ParticipantService, CancellationToken ct) =>
{
    var result = await ParticipantService.CreatePhonenumberToParticipantAsync(participantId, dto, ct);

    return Results.Created("/", result);
});


// READ
participants.MapGet("/getParticipant/{email}", async (string email, IParticipantService participantService, CancellationToken ct) =>
{
    var result = await participantService.GetParticipantByEmailAsync(email, ct);

    return Results.Ok(result);
});

participants.MapGet("/getParticipants", async (IParticipantService participantService, CancellationToken ct) =>
{
    var result = await participantService.GetAllParticipantsAsync(ct);

    return Results.Ok(result);
});

// UPDATE
participants.MapPut("/participants/{email}", async (string email, UpdateParticipantDTO dto, IParticipantService participantService, CancellationToken ct) =>
{
    var result = await participantService.UpdateParticipantAsync(email, dto, ct);

    return Results.Ok(result);
});

// DELETE
participants.MapDelete("/participants/{email}", async (string email, IParticipantService participantService, CancellationToken ct) =>
{
    var result = await participantService.DeleteParticipantAsync(email, ct);

    return result ? Results.NoContent() : Results.BadRequest();
});

participants.MapDelete("/{participantId}/phonenumbers/{phonenumberId}", async (Guid participantId, Guid phonenumberId, IParticipantService participantService, CancellationToken ct) =>
{
    var result = await participantService.DeletePhonenumberFromParticipantAsync(participantId, phonenumberId, ct);

    return result ? Results.NoContent() : Results.BadRequest();
});

#endregion
app.Run();