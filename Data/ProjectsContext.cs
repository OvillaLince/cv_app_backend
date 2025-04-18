using Microsoft.EntityFrameworkCore;
using MyApi.Models;

namespace MyApi.Data
{
    public class ProjectsContext : DbContext
    {
        public ProjectsContext(DbContextOptions<ProjectsContext> options) : base(options) { }

        public DbSet<DBProject> DBProjects { get; set; }
        public DbSet<DSProject> DSProjects { get; set; }

    }
}