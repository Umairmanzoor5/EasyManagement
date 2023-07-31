using EM.DataRepository.Projects;
using EM.Services;
using Microsoft.AspNetCore.Mvc;

namespace EM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _projectService.ListProjectTask());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpGet("Info/{id}")]
        public async Task<IActionResult> GetInfoStock(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                return Ok(await _projectService.InfoProjectTask(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Conflict("None of this products are available in stock!");
            }

        }

        [HttpGet("Approve/{id}")]
        public async Task<IActionResult> GetApprove(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _projectService.ApproveProjectTask(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Conflict("The project could not be approved!");
            }

        }

        [HttpGet("Recuse/{id}")]
        public async Task<IActionResult> GetRecuse(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _projectService.RecuseProjectTask(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Conflict("It was not possible to refuse the project!");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProject project)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _projectService.CreateProjectTask(project);

                return Created(string.Empty, project);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Conflict("VAT Number Conflict - " + project.Reference); //
            }

        }
    }
}
