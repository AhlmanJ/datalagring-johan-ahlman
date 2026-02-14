

// Here chatGPT has helped me implement Unit Of Work. See notes in the code + DbContext.

using EducationPlatform.Application.Abstractions.Persistence;
using EducationPlatform.Application.DTOs.Courses;
using EducationPlatform.Application.DTOs.Enrollments;
using EducationPlatform.Application.DTOs.Instructors;
using EducationPlatform.Application.DTOs.Lessons;
using EducationPlatform.Application.DTOs.Participants;
using EducationPlatform.Application.DTOs.Phonenumbers;
using EducationPlatform.Application.ServiceInterfaces;
using EducationPlatform.Application.Services;
using EducationPlatform.Domain.Interfaces;
using EducationPlatform.Domain.Repositories;
using EducationPlatform.Infrastructure.Data;
using EducationPlatform.Infrastructure.Repositories;
using EducationPlatform.Presentation.Api.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});

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
app.UseCors("AllowReact"); // To be able to use for example a REACT frontend.

app.UseExceptionHandler();

// I had to change the endpoints because i misunderstood and made a bit of mistakes with ":guid".

// And i also learned that i dont need a Query-parameter with the same info that i am mapping into,
// for example a "createDTO" in the same endpoint. Because then i allready have that parameter in the request.
// EndPoints:

#region Courses
var courses = app.MapGroup("/api/courses").WithTags("Courses");

// CREATE 

// Deleted "string Name" from this endpoint as i learned that i don't have to specify parameters that are included in my DTO. 
courses.MapPost("/", async (CreateCourseDTO dto, ICourseService courseService, CancellationToken ct) =>
{
    var result = await courseService.CreateCourseAsync(dto, ct);

    return Results.Created("/", result);
});

courses.MapPost("/createLesson/{courseId:guid}", async (Guid courseId, CreateLessonDTO dto, ICourseService courseService, CancellationToken ct) =>
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

courses.MapGet("/{courseId:guid}/lessons", async (Guid courseId, ICourseService courseService, CancellationToken ct) =>
{
    var result = await courseService.GetAllLessonByCourseIdAsync(courseId, ct);

    return Results.Ok(result);
});
        
courses.MapGet("/locations", async (ICourseService courseService, CancellationToken ct) =>
{ 
    var result = await courseService.GetAllLocationsAsync(ct);

    return Results.Ok(result);
});

// UPDATE
courses.MapPut("/{id:guid}", async (Guid id, UpdateCourseDTO dto, ICourseService courseService, CancellationToken ct) =>
{
    var result = await courseService.UpdateCourseAsync(id, dto, ct);
    return Results.Ok(result);
});

courses.MapPut("/lessons/{lessonId:guid}", async(Guid lessonId, UpdateLessonDTO updateLessonDTO, ICourseService courseService, CancellationToken ct) =>
{ 
    var result = await courseService.UpdateLessonAsync(lessonId, updateLessonDTO, ct);

    return Results.Ok(result);
});

// DELETE
courses.MapDelete("/{id:guid}", async (Guid id, ICourseService courseService, CancellationToken ct) =>
{
    await courseService.DeleteCourseAsync(id, ct);
});

courses.MapDelete("/lessons/{lessonId:guid}", async (Guid lessonId, ICourseService courseService, CancellationToken ct) =>
{
    var result = await courseService.DeleteLessonFromCourseAsync(lessonId, ct);

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
enrollments.MapPost("/", async (CreateEnrollmentDTO dto, Guid participantId, Guid lessonsId, IEnrollmentService enrollmentService, CancellationToken ct) =>
{
    var result = await enrollmentService.CreateEnrollmentAsync(dto, participantId, lessonsId, ct);

    return Results.Created("/", result);
});

// READ
enrollments.MapGet("/", async (IEnrollmentService enrollmentService, CancellationToken ct) =>
{
    var result = await enrollmentService.GetAllEnrollmentsAsync(ct);

    return Results.Ok(result);
});

// DELETE
enrollments.MapDelete("/{enrollmentId:guid}/{lessonName}/{participantId:guid}", async (Guid enrollmentId, string lessonName, Guid participantId, IEnrollmentService enrollmentService, CancellationToken ct) =>
{
    var result = await enrollmentService.DeleteEnrollmentAsync(enrollmentId, lessonName, participantId, ct);

    return result ? Results.NoContent() : Results.BadRequest();
});

#endregion



#region Instructors
var instructors = app.MapGroup("/api/instructors").WithTags("Instructors");

// CREATE
instructors.MapPost("/", async (CreateInstructorDTO dto, IInstructorService instructorService, CancellationToken ct) =>
{
    var result = await instructorService.CreateInstructorAsync(dto, ct);

    return Results.Created("/", result);
});

// READ
instructors.MapGet("/", async (IInstructorService instructorService, CancellationToken ct) =>
{
    var result = await instructorService.GetAllInstructorsAsync(ct);

    return Results.Ok(result);
});

// UPDATE
instructors.MapPut("/{Id:guid}", async (Guid id, UpdateInstructorDTO dto, IInstructorService instructorService, CancellationToken ct) =>
{
    var result = await instructorService.UpdateInstructorAsync(id, dto, ct);
    return Results.Ok(result);
});

instructors.MapPut("/{instructorId:guid}/lesson/{lessonId}", async (Guid instructorId, Guid lessonId, IInstructorService instructorService, CancellationToken ct) =>
{
    var result = await instructorService.EnrollInstructorToLessonAsync(lessonId, instructorId, ct);

    return Results.Ok(result);
});

// DELETE
instructors.MapDelete("/{Id:guid}", async (Guid id, IInstructorService instructorService, CancellationToken ct) =>
{
    var result = await instructorService.DeleteInstructorAsync(id, ct);

    return result ? Results.NoContent() : Results.BadRequest();
});

#endregion



#region Participants
var participants = app.MapGroup("/api/participants").WithTags("Participants");

// CREATE
participants.MapPost("/", async (CreateParticipantDTO dto, IParticipantService participantService, CancellationToken ct) =>
{
    var result = await participantService.CreateParticipantAsync(dto, ct);

    return Results.Created("/", result);
});

participants.MapPost("/CreatePhonenumber", async (Guid participantId, CreatePhonenumberDTO dto, IParticipantService ParticipantService, CancellationToken ct) =>
{
    var result = await ParticipantService.CreatePhonenumberToParticipantAsync(participantId, dto, ct);

    return Results.Created("/", result);
});


// READ
participants.MapGet("/{email}", async (string email, IParticipantService participantService, CancellationToken ct) =>
{
    var result = await participantService.GetParticipantByEmailAsync(email, ct);

    return Results.Ok(result);
});

participants.MapGet("/", async (IParticipantService participantService, CancellationToken ct) =>
{
    var result = await participantService.GetAllParticipantsAsync(ct);

    return Results.Ok(result);
});

// UPDATE
participants.MapPut("/{email}", async (string email, UpdateParticipantDTO dto, IParticipantService participantService, CancellationToken ct) =>
{
    var result = await participantService.UpdateParticipantAsync(email, dto, ct);

    return Results.Ok(result);
});

// DELETE
participants.MapDelete("/{email}", async (string email, IParticipantService participantService, CancellationToken ct) =>
{
    var result = await participantService.DeleteParticipantAsync(email, ct);

    return result ? Results.NoContent() : Results.BadRequest();
});

#endregion
app.Run();