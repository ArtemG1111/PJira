using MediatR;
using Microsoft.AspNetCore.Mvc;
using PJira.Application.Projects.Commands.CreateProject;
using PJira.Application.Projects.Commands.DeleteProject;
using PJira.Application.Projects.Commands.UpdateProject;
using PJira.Application.Projects.Queries.GetProjectById;
using PJira.Application.Projects.Queries.GetProjects;

namespace PJira.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProject()
        {
            var projects = await _mediator.Send(new GetProjectsQuery());

            return Ok(projects);
        }
        [HttpGet]
        [Route("getProjectById")]
        public async Task<IActionResult> GetProjectById(Guid id, GetProjectByIdQuery query)
        {
            if(id != query.Id)
            {
                return BadRequest();
            }

            var project = await _mediator.Send(query);

            return Ok(project);
        }

        [HttpPost]
        [Route("createProject")]
        public async Task<IActionResult> CreateProject(CreateProjectCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
        [HttpPut]
        [Route("updateProject")]
        public async Task<IActionResult> UpdateProject(Guid id, UpdateProjectCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            await _mediator.Send(command);

            return Ok();
        }
        [HttpDelete]
        [Route("deleteProject")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var command = new DeleteProjectCommand { Id = id};

            await _mediator.Send(command);

            return Ok();
        }
    }
}
