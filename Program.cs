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
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Use ONLY the correct CORS policy
app.UseCors();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
