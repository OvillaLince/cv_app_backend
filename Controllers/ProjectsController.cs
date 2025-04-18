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
		public IActionResult GetDbProjects()
		{
			var dbProjects = _context.DBProjects
				.ToList();

			return Ok(dbProjects);
		}

		[HttpGet("ds")]
		public IActionResult GetDsProjects()
		{
			var dsProjects = _context.DSProjects
				.ToList();

			return Ok(dsProjects);
		}
	}
}
