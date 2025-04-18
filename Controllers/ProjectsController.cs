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
        public IActionResult GetAllProjects()
        {

            var dbProjects = _context.DBProjects.ToList();
            var dsProjects = _context.DSProjects.ToList();

            var result = new
            {
                dbProjects,
                dsProjects
            };
            return Ok(result);
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
        public IActionResult Ping()
        {
			return Ok("pong");
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
