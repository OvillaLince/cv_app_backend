using Microsoft.EntityFrameworkCore;
using MyApi.Data; // replace with your actual namespace

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with PostgreSQL
builder.Services.AddDbContext<ProjectsContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    npgsqlOptions => npgsqlOptions.CommandTimeout(60)
    ));

// Add controllers
builder.Services.AddControllers();

// Enable CORS for all origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    options.AddPolicy("AllowFrontend",
       policy =>
       {
           policy.WithOrigins("https://ovillalince.github.io")
                 .AllowAnyMethod()
                 .AllowAnyHeader();
       });
});

var app = builder.Build();

app.UseCors("AllowAll");
app.UseCors("AllowFrontend");
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
