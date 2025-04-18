using MyApi.Data;
using Microsoft.EntityFrameworkCore; // already present
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL with 60s timeout
builder.Services.AddDbContext<ProjectsContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                      npgsqlOptions => npgsqlOptions.CommandTimeout(60)));

// Add controllers
builder.Services.AddControllers();
builder.Services.AddResponseCompression();
// CORS setup
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

// Handle global exceptions
app.UseExceptionHandler(appBuilder =>
{
    appBuilder.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"error\":\"Unexpected server error\"}");
    });
});
app.UseResponseCompression();
app.UseHttpsRedirection();           // Optional but recommended
app.UseCors("AllowFrontend");        //CORS must come before Authorization
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
