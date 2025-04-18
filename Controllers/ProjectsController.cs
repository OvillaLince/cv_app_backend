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

		[HttpGet("projects/db")]
		public IActionResult GetDbProjects()
		{
            var visitorLogPath = Path.Combine(Directory.GetCurrentDirectory(), "visitors.txt");

            // Get IP address or fallback to "Anon"
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Anon";

            try
            {
                // Log every access (even repeated ones) synchronously
                System.IO.File.AppendAllText(visitorLogPath, ipAddress + Environment.NewLine);
                Console.WriteLine($"[DS Access] IP logged: {ipAddress}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DS Access] Failed to log IP: {ex.Message}");
            }


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
            var path = Path.Combine(Directory.GetCurrentDirectory(), "visitors.txt");

            if (!System.IO.File.Exists(path))
            {
                return Ok(new
                {
                    totalVisitors = 0,
                    uniqueVisitors = 0,
                    visitorStats = new List<object>()
                });
            }

            var lines = System.IO.File.ReadAllLines(path);

            // Count number of accesses per IP
            var ipCounts = lines
                .GroupBy(ip => ip)
                .ToDictionary(group => group.Key, group => group.Count());

            var visitorStats = ipCounts
                .Select(entry => new
                {
                    ip = entry.Key,
                    accessCount = entry.Value
                })
                .ToList();

            return Ok(new
            {
                totalVisitors = lines.Length,
                uniqueVisitors = ipCounts.Count,
                visitorStats = visitorStats
            });
        }

    }
}
