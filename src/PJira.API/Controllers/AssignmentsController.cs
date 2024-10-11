﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using PJira.Application.Assignments.Commands.CreateTask;
using PJira.Application.Assignments.Commands.DeleteAssignment;
using PJira.Application.Assignments.Commands.UpdateAssignment;
using PJira.Application.Assignments.Queries.GetAssignment;
using PJira.Core.Models;

namespace PJira.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AssignmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssignmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAssignments()
        { 
            var dogs = await _mediator.Send(new GetAssignmentsQuery());

            return Ok(dogs);
        }
        [HttpPost]
        [Route("createAssignment/")]
        public async Task<IActionResult> CreateAssignment(CreateAssignmentCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
        [HttpPut]
        [Route("updateAssignment")]
        public async Task<IActionResult> UpdateAssignment(Guid id, UpdateAssignmentCommand command)
        {
            if(id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);

            return Ok();
        }
        [HttpDelete]
        [Route("deleteAssignment/")]
        public async Task<IActionResult> DeleteAssignment(Guid id)
        {
            var command = new DeleteAssignmentCommand { Id = id };

            await _mediator.Send(command);

            return Ok();
        }    

    }
}
