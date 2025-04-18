using Microsoft.AspNetCore.Mvc;
using MyApi.Data;
using MyApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
namespace MyApi.Controllers
{
	[ApiController]
	[Route("api")]
	public class ProjectsController : ControllerBase
	{
		private readonly ProjectsContext _context;
        private readonly IConfiguration _configuration;

        public ProjectsController(ProjectsContext context, IConfiguration configuration)
		{
			_context = context;
            _configuration = configuration;

        }

        [HttpGet("projects/all")]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                var dbProjects = await _context.DBProjects
                    .AsNoTracking()
                    .Select(p => new DBProject
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Image = p.Image,
                        CodeFile = p.CodeFile
                    })
                    .ToListAsync();

                var dsProjects = await _context.DSProjects
                    .AsNoTracking()
                    .Select(p => new DSProject
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Image = p.Image,
                        CodeFile = p.CodeFile
                    })
                    .ToListAsync();
                var results = new Projects { dbProject = dbProjects, dsProject = dsProjects };
                return Ok(results);
            }
            catch (Exception ex)
            {
                Console.WriteLine($" /projects/all failed: {ex.Message}");
                return StatusCode(500, new { error = ex.Message }); // ensures headers are returned
            }
        }


        [HttpGet("projects/db")]
		public IActionResult GetDbProjects()
		{

            var dbProjects = _context.DBProjects.ToList();

			return Ok(dbProjects);
		}

		[HttpGet("project/ds")]
		public IActionResult GetDsProjects()
		{
			var dsProjects = _context.DSProjects.ToList();

			return Ok(dsProjects);
		}


        [HttpGet("proj/ds/test")]
        public async Task<IActionResult> GetDSProjects()
        {
            using var conn = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var projects = await conn.QueryAsync<DSProject>("SELECT * FROM \"DSProjects\"");
            return Ok(projects);
        }

        [HttpHead("ping")]
        public async Task<IActionResult> PingDatabase()
        {
            try
            {
                // Simple DB test query
                var test = await _context.DBProjects.FirstOrDefaultAsync();
                return Ok(new { status = "Database is connected" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "Database unreachable", error = ex.Message });
            }
        }


        [HttpGet("visitors")]
        public IActionResult GetVisitorListAndAccessCounts()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "visitor_count.txt");

            int count = 0;

            if (System.IO.File.Exists(path))
            {
                var content = System.IO.File.ReadAllText(path);
                int.TryParse(content, out count);
            }

            return Ok(new
            {
                totalVisitors = count
            });
        }

    }
}
