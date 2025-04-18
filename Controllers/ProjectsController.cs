using Microsoft.AspNetCore.Mvc;
using MyApi.Data;
using MyApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MyApi.Controllers
{
	[ApiController]
	[Route("api/projects")]
	public class ProjectsController : ControllerBase
	{
		private readonly ProjectsContext _context;

		public ProjectsController(ProjectsContext context)
		{
			_context = context;
		}

		[HttpGet("db")]
		public async Task<IActionResult> GetDbProjects()
		{
			var dbProjects = await _context.DBProjects
				.ToListAsync();

			return Ok(dbProjects);
		}

		[HttpGet("ds")]
		public async Task<IActionResult> GetDsProjects()
		{
			var dsProjects = await _context.DSProjects
				.ToListAsync();

			return Ok(dsProjects);
		}
	}
}
