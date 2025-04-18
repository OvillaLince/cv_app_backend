using Microsoft.EntityFrameworkCore;
using MyApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with timeout
builder.Services.AddDbContext<ProjectsContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                      npgsqlOptions => npgsqlOptions.CommandTimeout(60))
);

// Add controllers
builder.Services.AddControllers();

// CORS policies
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://ovillalince.github.io")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Use ONLY the correct CORS policy
app.UseCors("AllowFrontend");

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
