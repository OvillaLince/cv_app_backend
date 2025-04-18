using Microsoft.AspNetCore.Mvc;
using MyApi.Data;
using MyApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MyApi.Controllers
{
	[ApiController]
	[Route("api")]
	public class ProjectsController : ControllerBase
	{
		private readonly ProjectsContext _context;

		public ProjectsController(ProjectsContext context)
		{
			_context = context;
		}

        [HttpGet("projects/all")]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                var dbProjects = await _context.DBProjects
                    .AsNoTracking()
                    .Select(p => new { p.Id, p.Title, p.Image, p.CodeFile })
                    .ToListAsync();

                var dsProjects = await _context.DSProjects
                    .AsNoTracking()
                    .Select(p => new { p.Id, p.Title, p.Image, p.CodeFile })
                    .ToListAsync();
                var results = new Projects { dbProject = dbProjects, dsProject = dsProjects };

                return Ok(results);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ /projects/all failed: {ex.Message}");
                return StatusCode(500, new { error = ex.Message }); // ensures headers are returned
            }
        }


        [HttpGet("projects/db")]
		public IActionResult GetDbProjects()
		{

            var dbProjects = _context.DBProjects.ToList();

			return Ok(dbProjects);
		}

		[HttpGet("projects/ds")]
		public IActionResult GetDsProjects()
		{
			var dsProjects = _context.DSProjects.ToList();

			return Ok(dsProjects);
		}

        [HttpGet("ping")]
        public async Task<IActionResult> PingDb()
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync();

                if (canConnect)
                    return Ok(new { status = "Supabase database is reachable" });

                return StatusCode(500, new { status = "Supabase not reachable" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PingDb error: {ex.Message}");
                return StatusCode(500, new { status = "Database error", error = ex.Message });
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
