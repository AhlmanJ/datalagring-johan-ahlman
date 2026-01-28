using EducationPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Registering DbContext.
builder.Services.AddDbContext<EducationPlatformDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("EducationPlatformDB"),
    // "Tells" where the migration files should be saved.
    sql => sql.MigrationsAssembly("EducationPlatform.Infrastructure")
    ));

var app = builder.Build();
 
app.MapOpenApi();
app.UseHttpsRedirection();

app.Run();
